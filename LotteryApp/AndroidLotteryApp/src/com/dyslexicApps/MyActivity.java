package com.dyslexicApps;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.*;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONObject;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.*;

public class MyActivity extends Activity
{
    public static final String CONFIG_FILE = "FloridaLotteryCheckerConfig";
    private ProgressDialog _waitDialog;
    private String _playerLotteryString;
    private LotteryNumbers _winningLotteryNumbers;
    private LotteryNumbers _playerLotteryNumbers;
    private LinkedList<Integer> _colors;
    private int _previousSum;
    private int _defaultEditTextBackgroundColor;

    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.lottery_checker);

        //Set the On Click Listener for the Submit Button
        final Button btnSubmit = getButton(R.id.btnSubmit);
        btnSubmit.setOnClickListener(new View.OnClickListener()
        {
            public void onClick(View v)
            {
                btnSubmit_onClick(v);
            }
        });

        //Set the On Click Listener for the About Button
        final Button btnAbout = getButton(R.id.btnAbout);
        btnAbout.setOnClickListener(new View.OnClickListener()
        {
            public void onClick(View v)
            {
                btnAbout_onClick(v);
            }
        });

        //Steer away focus from anything else especially the winning numbers
        getTextBox(R.id.txtPlayer1).requestFocus();

        StartUp();
    }

    /**
     * Initialization method
     */
    private void StartUp()
    {
        loadColors(); //List of colors used later to highlight each matching box

        loadPlayerLotteryNumberFromConfig(); //Load the saved lottery number from config into the _platerLotteryNumbers variable

        loadPlayerNumbers(_playerLotteryNumbers); //Load the player lottery numbers into their boxes

        //_defaultEditTextBackgroundColor = ((ColorDrawable)getTextBox(R.id.txtLotto1).getBackground()).getColor();
    }

    private void loadColors()
    {
        _colors = new LinkedList<Integer>();
        _colors.addLast(Color.parseColor("#808080")); //Similar to LightGoldenrodYellow
        _colors.addLast(Color.parseColor("#0000FF")); //Similar to LightCoral
        _colors.addLast(Color.parseColor("#008000")); //Similar to LightCyan
        _colors.addLast(Color.parseColor("#FFC0CB")); //Similar to LightGray
        _colors.addLast(Color.parseColor("#FF0000")); //Similar to LightGreen
        _colors.addLast(Color.parseColor("#FFD700")); //Similar to LightPink
    }

    /**
     * The main Entry Point of the program. The basic operations are:
     * Get the winning numbers for this period.
     * Take the provided player numbers and match them against the winning numbers.
     * @param v
     */
    public void btnSubmit_onClick(View v)
    {
        try
        {
            clearStatuses(); //Clear away any previous messages

            //Mark the time stamp of when the button was pressed
            setLabelText(R.id.lblTimeStamp, DateTime.Today().toString());

            //Save the entered player numbers
            savePlayerNumbers();

            //This method has to perform its main task asynchronously
            getWinningLotteryNumbers();
        }
        catch(LotteryException le)
        {
            showToastMessage(le.getMessage());
        }
        catch(Exception ex)
        {
            ErrorMessage(ex);
        }
    }

    public void btnAbout_onClick(View v)
    {
        showMessageBox("About Flottery", "The purpose of this application is to check your single set of Florida Lottery Numbers against the current drawn numbers. Comments, Questions or Bugs? Please email: DyslexicApps@gmail.com");
    }

    private void cbxSavePlayerNumbers_Checked(CompoundButton buttonView, boolean isChecked)
    {
        //CheckBox cbxSavePlayerNumbers = getCheckBox(R.id.cbxSavePlayerNumbers);

        if (isChecked)
        {
            try
            {
                savePlayerNumbers();
            }
            catch(LotteryException le)
            {
                showToastMessage(le.getMessage());
            }
        }
    }

    //This is strictly used for when loading saved numbers from a config file
    private void loadPlayerNumbers(LotteryNumbers numbers)
    {
        setTextBoxText(R.id.txtPlayer1, pad2(numbers.getNumber1()));
        setTextBoxText(R.id.txtPlayer2, pad2(numbers.getNumber2()));
        setTextBoxText(R.id.txtPlayer3, pad2(numbers.getNumber3()));
        setTextBoxText(R.id.txtPlayer4, pad2(numbers.getNumber4()));
        setTextBoxText(R.id.txtPlayer5, pad2(numbers.getNumber5()));
        setTextBoxText(R.id.txtPlayer6, pad2(numbers.getNumber6()));
    }

    private void loadWinningNumbers(LotteryNumbers numbers)
    {
        setTextBoxText(R.id.txtLotto1, pad2(numbers.getNumber1()));
        setTextBoxText(R.id.txtLotto2, pad2(numbers.getNumber2()));
        setTextBoxText(R.id.txtLotto3, pad2(numbers.getNumber3()));
        setTextBoxText(R.id.txtLotto4, pad2(numbers.getNumber4()));
        setTextBoxText(R.id.txtLotto5, pad2(numbers.getNumber5()));
        setTextBoxText(R.id.txtLotto6, pad2(numbers.getNumber6()));

        setLabelText(R.id.lblLottoJackpot, numbers.Jackpot);
        setLabelText(R.id.lblLottoStatus, numbers.Status);
    }

    private void clearStatuses()
    {
        TextView lblStatus = getLabel(R.id.lblStatus);
        lblStatus.setText("");
        lblStatus.setTextColor(Color.WHITE);

        for (int i = 0; i < 6; i++)
            resetTextBoxBackResource(getWinningBox(i), getPlayerBox(i));
    }

    private void savePlayerNumbers() throws LotteryException
    {
        _playerLotteryNumbers = getPlayerNumbers();

        _playerLotteryString = _playerLotteryNumbers.toString();

        //This method is my way of avoiding writing a bunch of listeners per player box (events)
        //it is a giant pain in the ass in java, so I don't want to do it.
        checkPlayerNumberSum();

        if (getCheckBox(R.id.cbxSavePlayerNumbers).isChecked())
        {
            setPlayerLotteryNumber(_playerLotteryString);

            showToastMessage("Your numbers have been saved");
        }
        else
            setPlayerLotteryNumber(""); //Purposely set this to empty string
    }

    private void getWinningLotteryNumbers()
    {
        try
        {
            //Show a TOAST message to let the user know what is going on
            _waitDialog = ProgressDialog.show(MyActivity.this, "Working", "Getting latest Lottery drawing, Please Wait...", true);

            //Temporarily disable the button so the user cannot double submit
            getButton(R.id.btnSubmit).setEnabled(false);

            //Get the service URI
            String strUrl = getRString(R.string.strWCFServiceURL);

            //If you want to avoid using the WCF service, comment out the first line and uncomment the second line

            //strUrl = ""; //Force an Error

            //A. This line will hit my WCF service
            new LottoProxy().execute(strUrl); //Execute the service call Asynchronously

            //B. This line is for quick debug only
            //setWinningLotteryNumbers("{\"Jackpot\":\"$2 Million\",\"Number\":\"03-08-09-12-33-52\",\"Status\":\"1 WINNER\"}");
        }
        catch (Exception ex)
        {
            ErrorMessage(ex);

            resetControlStates();
        }
    }

    /**
     * This method takes the JSON Result, loads its properties into the appropriate
     * places and loads the winning lottery numbers into their boxes. The set of
     * numbers (player and winning) are cross examined for matches. Any matches
     * are marked with the same color for easy viewing.
     * @param jsonResult
     */
    private void setWinningLotteryNumbers(String jsonResult)
    {
        try
        {
            if(jsonResult != null && !isEmpty(jsonResult))
            {
                JSONObject obj = new JSONObject(jsonResult);

                LotteryInfo info = new LotteryInfo();

                info.Jackpot = obj.getString("Jackpot");
                info.Number = obj.getString("Number");
                info.Status = obj.getString("Status");

                setLabelText(R.id.lblLottoJackpot, info.Jackpot);
                setLabelText(R.id.lblLottoStatus, info.Status);

                _winningLotteryNumbers = new LotteryNumbers(info.Number);
                _winningLotteryNumbers.Jackpot = info.Jackpot;
                _winningLotteryNumbers.Status = info.Status;

                loadWinningNumbers(_winningLotteryNumbers);

                if (_playerLotteryNumbers.hasDuplicateValues())
                {
                    TextView lblStatus = getLabel(R.id.lblStatus);

                    lblStatus.setText("The lottery numbers you entered contained duplicate numbers!");
                    lblStatus.setTextColor(Color.RED);
                }
                else
                    checkForMatches(LotteryNumbers.crossExamineSets(_winningLotteryNumbers, _playerLotteryNumbers));
            }
            else
                ErrorMessage("The Lottery Checking Service is currently unreachable. Please try again later.");
        }
        catch(Exception ex)
        {
            ErrorMessage(ex);
        }
        finally
        {
            resetControlStates();
        }
    }


    private LotteryNumbers getPlayerNumbers() throws LotteryException
    {
        int intConverted = 0;
        String strNumber = "";
        EditText txt = null;
        Reason exReason = Reason.InvalidString;
        LotteryNumbers ln = new LotteryNumbers();
        ArrayList<Integer> lstValues = new ArrayList<Integer>();

        for (int i = 0; i < 6; i++)
        {
            txt = getPlayerBox(i); //Get the player box

            strNumber = txt.getText().toString().trim(); //Get the string that was entered

            try
            {
                //Setting this up just in case
                exReason = Reason.InvalidString;

                if(isEmpty(strNumber))
                {
                    exReason = Reason.EmptyString;

                    throw new Exception("Values cannot be blank!");
                }

                //Attempt to convert the string to a valid integer, because it could be:
                // 1. Empty
                // 2. Not a Number (NaN)
                // 3. Negative (Auto Corrected)
                // 4. Out of range
                intConverted = Integer.parseInt(strNumber);

                if(intConverted < 0)
                {
                    intConverted = Math.abs(intConverted);

                    txt.setText(Integer.toString(intConverted));
                }

                //The range for lottery is between 1 and 53
                if(intConverted == 0 || intConverted > 53)
                {
                    exReason = Reason.InvalidRange;

                    throw new Exception("Lottery numbers must be between 1 and 53 inclusive.");
                }

                //Keep track of what was added
                if(!lstValues.contains(intConverted))
                    lstValues.add(intConverted);
                else
                {
                    exReason = Reason.DuplicateFound;

                    throw new Exception("Duplicate values are not allowed!");
                }
            }
            catch(Exception ex)
            {
                String exMessage = "Not a valid number!";

                txt.setBackgroundColor(Color.RED);

                if(exReason != Reason.InvalidString)
                    exMessage = ex.getMessage();

                throw new LotteryException(exReason, exMessage);
            }

            //Store the final value
            ln.setNumber(i, intConverted);
        }

        return ln;
    }

    private void setCheckBoxListenerState(boolean enabled)
    {
        final CheckBox cbx = getCheckBox(R.id.cbxSavePlayerNumbers);

        if(enabled)
        {
            cbx.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener()
            {
                public void onCheckedChanged(CompoundButton buttonView, boolean isChecked)
                {
                    cbxSavePlayerNumbers_Checked(buttonView, isChecked);
                }
            });
        }
        else
            cbx.setOnCheckedChangeListener(null);
    }

    private void loadPlayerLotteryNumberFromConfig()
    {
        try
        {
            //cbxSavePlayerNumbers.Checked -= cbxSavePlayerNumbers_Checked;
            setCheckBoxListenerState(false);

            //Get the lottery string from the local app config
            _playerLotteryString = getPlayerLotteryNumber();

            //These are test numbers. Obviously should not remain hard coded.
            //My Numbers: 01-17-23-31-44-53
            //_playerLotteryString = "05-06-07-08-09-10";

            CheckBox cbxSavePlayerNumbers = getCheckBox(R.id.cbxSavePlayerNumbers);

            cbxSavePlayerNumbers.setChecked(!IsNullOrWhiteSpace(_playerLotteryString));

            //If the config value is not null or white space
            if (cbxSavePlayerNumbers.isChecked())
            {
                //Attempt to parse the lottery string
                //TODO: Need to check if the string is valid
                //TODO: Need to make sure the conversion doesn't fail
                _playerLotteryNumbers = LotteryNumbers.ParseLotteryNumberString(_playerLotteryString);
            }
            else
                _playerLotteryNumbers = new LotteryNumbers();

            _previousSum = _playerLotteryNumbers.sum();
        }
        catch (Exception ex)
        {
            ex.toString();
        }
        finally
        {
            //cbxSavePlayerNumbers.Checked += cbxSavePlayerNumbers_Checked;
            setCheckBoxListenerState(true);
        }
    }

    private void resetControlStates()
    {
        if(_waitDialog != null)
            _waitDialog.hide();

        getButton(R.id.btnSubmit).setEnabled(true);
    }

    private void resetTextBoxBackResource(EditText winning, EditText player)
    {
        winning.setBackgroundResource(android.R.drawable.editbox_background_normal);
        player.setBackgroundResource(android.R.drawable.editbox_background);
    }

    private void setTextBoxBackColor(EditText winning, EditText player, int backColor)
    {
        winning.setBackgroundColor(backColor);
        player.setBackgroundColor(backColor);
    }

    private EditText getWinningBox(int boxNumber)
    {
        EditText box = getTextBox(R.id.txtLotto1); //I don't like this

        switch (boxNumber)
        {
            case 0:
                box = getTextBox(R.id.txtLotto1);
                break;

            case 1:
                box = getTextBox(R.id.txtLotto2);
                break;

            case 2:
                box = getTextBox(R.id.txtLotto3);
                break;

            case 3:
                box = getTextBox(R.id.txtLotto4);
                break;

            case 4:
                box = getTextBox(R.id.txtLotto5);
                break;

            case 5:
                box = getTextBox(R.id.txtLotto6);
                break;
        }

        return box;
    }

    private EditText getPlayerBox(int boxNumber)
    {
        EditText box = getTextBox(R.id.txtPlayer1); //I don't like this

        switch (boxNumber)
        {
            case 0:
                box = getTextBox(R.id.txtPlayer1);
                break;

            case 1:
                box = getTextBox(R.id.txtPlayer2);
                break;

            case 2:
                box = getTextBox(R.id.txtPlayer3);
                break;

            case 3:
                box = getTextBox(R.id.txtPlayer4);
                break;

            case 4:
                box = getTextBox(R.id.txtPlayer5);
                break;

            case 5:
                box = getTextBox(R.id.txtPlayer6);
                break;
        }

        return box;
    }

    private void checkForMatches(Map<Integer, Integer> matches)
    {
        try
        {
            setLabelText(R.id.lblNumbersHit, String.valueOf(matches.size()) + " of 6");

            Iterator<Integer> itrColors = _colors.iterator();

            for (Map.Entry<Integer, Integer> kvp : matches.entrySet())
                setTextBoxBackColor(getWinningBox(kvp.getKey()), getPlayerBox(kvp.getValue()), itrColors.next());

            setPlayerStatus(matches.size());
        }
        catch(Exception ex)
        {
            ex.toString();
        }
    }

    private void setPlayerStatus(int numberOfMatches)
    {
        float size = 0;
        String status = "";
        int intensity;


        switch (numberOfMatches)
        {
            case 6:
                status = "Congrats you are now wealthy!!!!";
                intensity = Color.parseColor("#FFD700"); //GOLD
                size = 22;
                break;
            case 5:
                status = "Not what I was looking for, but hey I'll take it!";
                intensity = Color.GREEN;
                size = 20;
                break;
            case 4:
                status = "At least it's something...";
                intensity = Color.YELLOW;
                size = 18;
                break;
            case 3:
                status = "Better than a free ticket I guess...";
                intensity = Color.parseColor("#40E0D0"); //Turquoise
                size = 16;
                break;
            case 2:
                status = "Oh goody a free drawing... only if you paid for XTRA...";
                intensity = Color.GRAY;
                size = 14;
                break;
            default:
                status = "Better luck next time.";
                intensity = Color.WHITE;
                size = 12;
                break;
        }

        TextView lblPlayerStatus = getLabel(R.id.lblPlayerStatus);

        lblPlayerStatus.setText(status);
        lblPlayerStatus.setTextColor(intensity);
        lblPlayerStatus.setTextSize(size);
    }

    //Essentially if the user changed the numbers they will sum up to something different.
    //This is being used to check if they changed their numbers or not
    private void checkPlayerNumberSum()
    {
        int newSum = _playerLotteryNumbers.sum();

        if(newSum != _previousSum)
        {
            getCheckBox(R.id.cbxSavePlayerNumbers).setChecked(false);

            _previousSum = newSum;

            showToastMessage("You changed your numbers! \nCheck the box again to save the new set.");
        }
    }

    //------------------------------ Re-usable Utility Methods ------------------------------------
    //Only Android Level 9 Supports String.isEmpty();
    //http://stackoverflow.com/questions/5244927/cant-call-string-isempty-in-android
    public boolean isEmpty(String target)
    {
        return target.length() == 0;
    }

    public void ToastError(Exception ex)
    {
        ToastError(ex.getMessage());
    }

    public void ToastError(String message)
    {
        showToastMessage("Error: " + message + "\n" + "Bugs?! - Please press the About Button.");
    }

    public void ErrorMessage(Exception ex)
    {
        ErrorMessage(ex.getMessage());
    }

    public void ErrorMessage(String message)
    {
        showMessageBox("A problem has occurred", "Error: " + message + "\n" + "Bugs?! - Please press the About Button.");
    }

    public boolean IsNullOrWhiteSpace(String target)
    {
        return (target == null || isEmpty(target));
    }

    //I want to make my version of a ConfigurationManager for java
    public String getPlayerLotteryNumber()
    {
        SharedPreferences settings = getSharedPreferences(CONFIG_FILE, 0);

        return settings.getString("PlayerLotteryNumber", _playerLotteryString);
    }

    public void setPlayerLotteryNumber(String value)
    {
        SharedPreferences settings = getSharedPreferences(CONFIG_FILE, 0);
        SharedPreferences.Editor editor = settings.edit();

        editor.putString("PlayerLotteryNumber", value);
        editor.commit();
    }

    private void setTextBoxText(int id, String text)
    {
        EditText blah = ((EditText)findViewById(id));
        blah.setText(text);

    }

    private void setLabelText(int id, String text)
    {
        getLabel(id).setText(text);
    }

    private CheckBox getCheckBox(int id)
    {
        return ((CheckBox)findViewById(id));
    }

    private TextView getLabel(int id)
    {
        return ((TextView)findViewById(id));
    }

    private EditText getTextBox(int id)
    {
        return ((EditText)findViewById(id));
    }

    private void showMessageBox(String title, String message)
    {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);

        builder.setTitle(title)
                .setMessage(message)
                .setCancelable(false)
                .setNegativeButton("Close", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });

        AlertDialog alert = builder.create();

        alert.show();
    }

    private void showToastMessage(String text)
    {
        Toast.makeText(getApplicationContext(), text, Toast.LENGTH_LONG).show();
    }

    private String getRString(int id)
    {
        return getResources().getString(id);
    }

    private Button getButton(int id)
    {
        return (Button) findViewById(id);
    }

    private String pad2(int number)
    {
        return String.format("%02d", number);
    }

    //This is an inner class
    private class LottoProxy extends AsyncTask<String, Void, String>
    {
        private Exception exception;

        protected String doInBackground(String... url)
        {
            try
            {
                HttpGet request = new HttpGet(url[0] + "/GetTodaysWinningNumber");
                request.setHeader("Accept", "application/json");
                request.setHeader("Content-type", "application/json");

                DefaultHttpClient httpClient = new DefaultHttpClient();
                HttpResponse response = httpClient.execute(request);

                HttpEntity responseEntity = response.getEntity();

                // Read response data into buffer
                char[] buffer = new char[(int)responseEntity.getContentLength()];
                InputStream stream = responseEntity.getContent();
                InputStreamReader reader = new InputStreamReader(stream);
                reader.read(buffer);
                stream.close();

                return new String(buffer);
            }
            catch (Exception e)
            {
                this.exception = e;

                return null;
            }
        }

        /**
         * After the Asynchronous task completes this method is called to do something
         * with the service result.
         * @param jsonResult
         */
        protected void onPostExecute(String jsonResult)
        {
            setWinningLotteryNumbers(jsonResult);
        }
    }

    private enum Reason
    {
        EmptyString,
        InvalidString,
        InvalidRange,
        DuplicateFound
    }

    public class LotteryException extends Exception
    {
        private String message = null;
        private Reason _reason = Reason.InvalidRange;

        public LotteryException(Reason reason)
        {
            super(reason.toString());
            _reason = reason;
            this.message = reason.toString();
        }

        public LotteryException(Reason reason, String message)
        {
            super(message);
            _reason = reason;
            this.message = message;
        }

        public LotteryException(Throwable cause)
        {
            super(cause);
        }

        @Override
        public String toString()
        {
            return message;
        }

        @Override
        public String getMessage()
        {
            return message;
        }

        public Reason getReason()
        {
            return _reason;
        }

        public void Handle()
        {
            showToastMessage(message);
        }
    }
}
