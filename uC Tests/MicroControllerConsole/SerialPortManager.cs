using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace MicroControllerConsole
{
    public class SerialPortManager
    {
        public const string CRLF = "\r\n";

        public string XON { get; private set; }
        public string XOFF { get; private set; }
        public string STX { get; private set; }
        public string ETX { get; private set; }

        public SerialPort Port { get; private set; }

        public bool Sleep { get; private set; }
        public string TX { get; private set; }
        public string RX { get; private set; }

        private StringBuilder Responses { get; set; }

        public SerialPortManager(SerialPort port)
        {
            if (port == null)
                throw new ArgumentNullException("You must instantiate your port before passing it in.");

            //http://en.wikipedia.org/wiki/Software_flow_control
            XON = GetCharacter(17);
            XOFF = GetCharacter(19);
            STX = GetCharacter(2);
            ETX = GetCharacter(3);
            
            Port = port;
            
            Open();
        }

        public static string GetCharacter(int decValue)
        {
            return new string(Encoding.UTF8.GetChars(new byte[] { (byte)decValue }));
        }

        public void Open()
        {
            if (!Port.IsOpen)
                Port.Open();
        }

        public void Close()
        {
            if (Port.IsOpen)
                Port.Close();
        }

        public string Conversation(string input, int waitInSeconds = 1)
        {
            string strData = null;

            try
            {
                Port.DataReceived += Port_DataReceived;

                Responses = new StringBuilder();

                Sleep = true;

                Write(input);

                while (Sleep)
                    Wait(5);

                strData = Responses.ToString();

                if (strData.Contains(STX))
                    strData = strData.Substring(strData.IndexOf(STX) + 1).TrimEnd(ETX.ToCharArray());

                Console.WriteLine();
                Console.WriteLine("Final Output: " + strData);
            }
            catch (Exception ex)
            {
                strData = ex.Message;
            }
            finally
            {
                Port.DataReceived -= Port_DataReceived;
            }

            return strData;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine("DRE: " + Read());
            string strData = Read();

            Responses.Append(strData);

            if (strData.Contains(ETX))
                Sleep = false;
        }

        public void ClearBuffers()
        {
            Port.DiscardInBuffer();
            Port.DiscardOutBuffer();
        }

        public void Write(string input)
        {
            TX = input;

            ClearBuffers();

            Port.Write(Encoding.UTF8.GetBytes(TX), 0, TX.Length);

            Console.WriteLine("Tx: " + input);
        }

        public string Read(int waitInSeconds = 1)
        {
            int seconds = 0;
            int minutes = 0;
            TimeSpan ts;

            try
            {
                Debug.Print("Rx listening");

                //Wait until there is something new to read
                while (Port.BytesToRead <= 0)
                {
                    Wait(waitInSeconds);

                    seconds += waitInSeconds;

                    ts = new TimeSpan(0, 0, seconds);

                    if (ts.Minutes > minutes)
                    {
                        minutes = ts.Minutes;

                        Debug.Print(minutes.ToString() + " min");
                    }
                }

                return ReadData();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);

                throw;
            }
        }

        private string ReadData()
        {
            byte[] arr = new byte[Port.BytesToRead];

            Port.Read(arr, 0, Port.BytesToRead);

            RX = new string(Encoding.UTF8.GetChars(arr));

            Debug.Print("Rx: [" + RX + "]");

            return RX;
        }

        public void DeviceTxWait()
        {
            Write(XOFF);
        }

        public void DeviceTxContinue()
        {
            Write(XON);
        }

        public void Wait(int seconds = 1)
        {
            for (int i = 0; i < seconds; i++)
                Thread.Sleep(1000); //1000 milliseconds = 1 second
        }
    }
}
