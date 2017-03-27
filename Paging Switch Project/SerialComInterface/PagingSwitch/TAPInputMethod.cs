using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using ServerOps;

namespace PagingSwitchLibrary.PagingSwitch
{
    public class TAPInputMethod : PagerControllerBase, IPagerController
    {
        private string _response;

        public TAPInputMethod() : base()
        { 
            //Starts up the connection to the serial port
        }

        private string Connect()
        {
            //Send that first <LF> here
            return SubmitCommand(CR); //This is what I expect back from the server
        }

        private string SubmitCommand(string command)
        {
            return WriteToPort(command, 1).Trim();
        }

        #region IPagerController Members
        public bool ConnectToPagingSwitch()
        {
            bool success = false;
            //AppendResult(_sb, "CMD: " + commandAfterResponse);

            _response = OpenSerialConnection();

            if (_response.Contains("ID=")) //ID=\r
            {
                Thread.Sleep(1000);

                _response = SubmitCommand(ESC + "PG1" + CR);

                if (_response.Contains(ACK))
                    success = true;
            }

            return success;
        }

        public void CloseConnection()
        {
            SubmitCommand(EOT + CR);
            Close();
        }

        public string SendPage(string subscriber, string message)
        {
            string strTransaction = string.Empty,
                   strCheckSum = string.Empty;

            //This is part of the transaction to be sent
            strTransaction = STX + subscriber + CR + message + CR + ETX;
            
            //This is the other part of the transaction
            strCheckSum = GenerateCheckSum(strTransaction);

            //The full transaction is finally sent after the checksum is computed
            _response = SubmitCommand(strTransaction + strCheckSum + CR);

            //Example: <STX>123<CR>ABC<CR><ETX>17;<CR>
            //ASCII  : [2]123[13]ABC[13][3]17;[13]

            return _response;
        }

        public string GenerateCheckSum(string wholePagerMessage)
        {
            int intDecimalTotal = 0,
                intNibbleAsDecimal = 0,
                j = 0;
            char[] arrChar = null;
            string strBinary = string.Empty,
                   strCheckSum = string.Empty,
                   strNibble = string.Empty;
            List<char> lstBits = null,
                       lstNibble = null;
            List<string> lstCheckSum = null;

            //Get the characters of the message
            arrChar = wholePagerMessage.ToCharArray();

            //Get each character's decimal value and total it
            foreach (char c in arrChar)
                intDecimalTotal += c;

            //Convert the total to a binary number
            strBinary = Convert.ToString(intDecimalTotal, 2);

            //Convert the binary string back to a char array of bits
            lstBits = new List<char>(strBinary.ToCharArray());

            //Reverse the order of the bits for easier reading since
            //reading goes from right (most significant) to left (least significant)
            lstBits.Reverse();

            lstNibble = new List<char>();
            lstCheckSum = new List<string>();

            //Read each nibble of the binary string (previously a decimal sum)
            //convert it to a decimal value and add 48 to it.
            for (int i = 0; i < lstBits.Count; i++)
            {
                //Get each character up to 4 which is a nibble 
                lstNibble.Add(lstBits[i]);

                j++;

                //If this is 4 characters OR this is the last element
                if (j == 4 || i == lstBits.Count - 1)
                {
                    j = 0; //Restart count

                    lstNibble.Reverse(); //Reverse this again

                    //Convert this to a string
                    strNibble = new string(lstNibble.ToArray());

                    //Add 48 to the decimal value of this nibble
                    intNibbleAsDecimal = Convert.ToInt32(strNibble, 2) + 48;

                    lstCheckSum.Add(Utils.GetChar((byte)intNibbleAsDecimal));

                    lstNibble = new List<char>(); //Reset
                }
            }

            lstCheckSum.Reverse(); //Reverse ONE MORE TIME

            //Loop through each string (character) and create the check sum string
            foreach (string s in lstCheckSum)
                strCheckSum += s;

            return strCheckSum;
        }

        #endregion
    }
}
