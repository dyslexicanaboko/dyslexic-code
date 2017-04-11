using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LotteryBaseLogic;
using LotteryApp.LottoService;

namespace LotteryApp
{
    /// <summary>
    /// Interaction logic for LotteryWindow.xaml
    /// </summary>
    public partial class LotteryWindow : Window
    {
        private bool _loading;
        private string _lotteryURI;
        private string _playerLotteryString;
        private LotteryNumbers _winningLotteryNumbers;
        private LotteryNumbers _playerLotteryNumbers;
        private LinkedList<Brush> _colors;
        private LotteryServiceClient _client;

        private string PlayerLotteryNumber
        {
            get { return UserConfig.Default.PlayerLotteryNumber; }
            set 
            {
                UserConfig.Default.PlayerLotteryNumber = value;
                UserConfig.Default.Save();
            }
        }

        public LotteryWindow()
        {
            InitializeComponent();

            _loading = true;

            _lotteryURI = ConfigurationManager.AppSettings["LotteryURI"];
           
            _client = new LotteryServiceClient();

            LoadColors();

            LoadPlayerLotteryNumberFromConfig();

            LoadPlayerNumbers(_playerLotteryNumbers);

            _loading = false;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearStatuses();

                lblTimeStamp.Content = DateTime.Now.ToString();

                SavePlayerNumbers();

                btnSubmit.IsEnabled = false;

                pbarServiceCall.IsIndeterminate = true;

                _client.BeginGetTodaysWinningNumber(HandleAsyncResult, _client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                btnSubmit.IsEnabled = true;

                pbarServiceCall.IsIndeterminate = false;
            }
        }

        private void HandleAsyncResult(IAsyncResult ar)
        {
            try
            {
                LotteryInfo info = _client.EndGetTodaysWinningNumber(ar);

                _winningLotteryNumbers = new LotteryNumbers(info.Number);
                _winningLotteryNumbers.Jackpot = info.Jackpot;
                _winningLotteryNumbers.Status = info.Status;

                this.Dispatcher.BeginInvoke((Action)delegate()
                {
                    LoadWinningNumbers(_winningLotteryNumbers);

                    if (_playerLotteryNumbers.HasDuplicateValues())
                    {
                        lblStatus.Content = "The lottery numbers you entered contained duplicate numbers!";
                        lblStatus.Foreground = Brushes.Red;
                    }
                    else
                        CheckForMatches(LotteryNumbers.CrossExamineSets(_winningLotteryNumbers, _playerLotteryNumbers));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                this.Dispatcher.BeginInvoke((Action)delegate()
                {
                    btnSubmit.IsEnabled = true;

                    pbarServiceCall.IsIndeterminate = false;
                });
            }
        }

        private void ClearStatuses()
        {
            lblStatus.Content = string.Empty;
            lblStatus.Foreground = Brushes.Black;

            for (int i = 0; i < 6; i++)
                SetTextBoxBackColor(GetWinningBox(i), GetPlayerBox(i), Brushes.White);
        }

        private void LoadPlayerLotteryNumberFromConfig()
        {
            try
            {
                cbxSavePlayerNumbers.Checked -= cbxSavePlayerNumbers_Checked;

                //Get the lottery string from the local app config
                _playerLotteryString = PlayerLotteryNumber;

                cbxSavePlayerNumbers.IsChecked = !string.IsNullOrWhiteSpace(_playerLotteryString);

                //If the config value is not null or white space
                if (cbxSavePlayerNumbers.IsChecked == true)
                {
                    //Attempt to parse the lottery string
                    //TODO: Need to check if the string is valid
                    //TODO: Need to make sure the conversion doesn't fail
                    _playerLotteryNumbers = LotteryNumbers.ParseLotteryNumberString(_playerLotteryString);
                }
                else
                    _playerLotteryNumbers = new LotteryNumbers();
            }
            finally
            {
                cbxSavePlayerNumbers.Checked += cbxSavePlayerNumbers_Checked;
            }
        }

        private void LoadColors()
        {
            _colors = new LinkedList<Brush>();
            _colors.AddLast(Brushes.LightGoldenrodYellow);
            _colors.AddLast(Brushes.LightCoral);
            _colors.AddLast(Brushes.LightCyan);
            _colors.AddLast(Brushes.LightGray);
            _colors.AddLast(Brushes.LightGreen);
            _colors.AddLast(Brushes.LightPink);                
        }

        private void LoadWinningNumbers(LotteryNumbers numbers)
        {
            txtLotto1.Text = numbers.Number1.ToString("00");
            txtLotto2.Text = numbers.Number2.ToString("00");
            txtLotto3.Text = numbers.Number3.ToString("00");
            txtLotto4.Text = numbers.Number4.ToString("00");
            txtLotto5.Text = numbers.Number5.ToString("00");
            txtLotto6.Text = numbers.Number6.ToString("00");

            tbLottoJackpot.Text = numbers.Jackpot;
            tbLottoStatus.Text = numbers.Status;
        }

        private void LoadPlayerNumbers(LotteryNumbers numbers)
        {
            txtPlayer1.Text = numbers.Number1.ToString("00");
            txtPlayer2.Text = numbers.Number2.ToString("00");
            txtPlayer3.Text = numbers.Number3.ToString("00");
            txtPlayer4.Text = numbers.Number4.ToString("00");
            txtPlayer5.Text = numbers.Number5.ToString("00");
            txtPlayer6.Text = numbers.Number6.ToString("00");
        }

        private LotteryNumbers GetPlayerNumbers()
        { 
            LotteryNumbers ln = new LotteryNumbers();

            for (int i = 0; i < 6; i++)
                ln.SetNumber(i, GetPlayerBox(i).Text);

            return ln;
        }

        private void SavePlayerNumbers()
        {
            _playerLotteryNumbers = GetPlayerNumbers();

            _playerLotteryString = _playerLotteryNumbers.ToString();

            if (cbxSavePlayerNumbers.IsChecked == true)
                PlayerLotteryNumber = _playerLotteryString;
        }

        private void CheckForMatches(Dictionary<int, int> matches)
        {
            lblNumbersHit.Content = matches.Count.ToString() + " of 6";

            LinkedList<Brush>.Enumerator iter = _colors.GetEnumerator();
            iter.MoveNext();

            foreach (KeyValuePair<int, int> kvp in matches)
            {
                SetTextBoxBackColor(GetWinningBox(kvp.Key), GetPlayerBox(kvp.Value), iter.Current);

                iter.MoveNext();
            }

            SetPlayerStatus(matches.Count);
        }

        //TODO: Think about consolidating this logic into the base logic project
        private void SetPlayerStatus(int numberOfMatches)
        {
            double size = 0;
            string status = string.Empty;
            Brush intensity;

            switch (numberOfMatches)
            { 
                case 6:
                    status = "Congrats you are now wealthy!!!!";
                    intensity = Brushes.Gold;
                    size = 22;
                    break;
                case 5:
                    status = "Not what I was looking for, but hey I'll take it!";
                    intensity = Brushes.Green;
                    size = 20;
                    break;
                case 4:
                    status = "At least it's something...";
                    intensity = Brushes.Yellow;
                    size = 18;
                    break;
                case 3:
                    status = "Better than a free ticket I guess...";
                    intensity = Brushes.Turquoise;
                    size = 16;
                    break;
                case 2:
                    status = "Oh goody a free drawing... only if you paid for XTRA...";
                    intensity = Brushes.Gray;
                    size = 14;
                    break;
                default:
                    status = "Better luck next time.";
                    intensity = Brushes.Black;
                    size = 12;
                    break;
            }

            lblPlayerStatus.Content = status;
            lblPlayerStatus.Foreground = intensity;
            lblPlayerStatus.FontSize = size;
        }

        private TextBox GetWinningBox(int boxNumber)
        {
            TextBox box = new TextBox();

            switch (boxNumber)
            { 
                case 0:
                    box = txtLotto1;
                    break;

                case 1:
                    box = txtLotto2;
                    break;

                case 2:
                    box = txtLotto3;
                    break;

                case 3:
                    box = txtLotto4;
                    break;

                case 4:
                    box = txtLotto5;
                    break;

                case 5:
                    box = txtLotto6;
                    break;
            }

            return box;
        }

        private TextBox GetPlayerBox(int boxNumber)
        {
            TextBox box = new TextBox();

            switch (boxNumber)
            {
                case 0:
                    box = txtPlayer1;
                    break;

                case 1:
                    box = txtPlayer2;
                    break;

                case 2:
                    box = txtPlayer3;
                    break;

                case 3:
                    box = txtPlayer4;
                    break;

                case 4:
                    box = txtPlayer5;
                    break;

                case 5:
                    box = txtPlayer6;
                    break;
            }

            return box;
        }

        private void SetTextBoxBackColor(TextBox winning, TextBox player, Brush backColor)
        {
            winning.Background = backColor;
            player.Background = backColor;
        }

        private void cbxSavePlayerNumbers_Checked(object sender, RoutedEventArgs e)
        {
            if (cbxSavePlayerNumbers.IsChecked == true)
                SavePlayerNumbers();
        }

        private void Player_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_loading)
            {
                int intValue = 0;

                TextBox tb = (TextBox)sender;

                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "00";
                else if (!int.TryParse(tb.Text, out intValue) || (intValue < 0 || intValue > 53))
                    MessageBox.Show("You must enter an integer between 1 and 53.");

                if (cbxSavePlayerNumbers != null)
                    cbxSavePlayerNumbers.IsChecked = false;
            }
        }

        private void Player_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            tb.SelectAll();
        }
    }
}
