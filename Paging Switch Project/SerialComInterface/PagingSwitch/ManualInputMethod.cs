using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace PagingSwitchLibrary.PagingSwitch
{
    public class ManualInputMethod : PagerControllerBase, IPagerController
    {
        private string _response;

        public ManualInputMethod() : base()
        {
            //Starts up the connection to the serial port
        }

        public bool ConnectToPagingSwitch()
        {
            bool success = false;
            //AppendResult(_sb, "CMD: " + commandAfterResponse);

            _response = Connect().Trim();

            if (_response == "ID=")
            {
                _response = SubmitCommand("M").Trim();

                if (_response == "logon granted\r\r\n\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0--- Page ---\r\n\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0Subscriber:")
                    success = true;
            }

            return success;
        }

        public void CloseConnection()
        {
            Close();
        }

        public string SendPage(string subscriber, string message)
        {
            int intMaxMessageSize = 500;

            _response = "Message Not Sent";

            try
            {
                //Make sure there is a subscriber, if not complain
                if (string.IsNullOrEmpty(subscriber))
                    return "Invalid Subscriber Provided";

                subscriber = subscriber.Trim();

                //Make sure the message isn't blank or empty
                if (string.IsNullOrEmpty(message))
                    message = "blank message"; //If so fill it

                message = message.Trim();

                //Make sure that the message size isn't above 500 characters
                if (message.Length > intMaxMessageSize)
                    message = message.Remove(intMaxMessageSize - 1); //If so, truncate it

                SubmitCommand("\r").Trim();
                SubmitCommand(subscriber).Trim();
                SubmitCommand(message).Trim();

                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
            finally
            {
                //Close();
            }

            return _response;
        }

        private string Connect()
        {
            //Send that first <LF> here
            return SubmitCommand(CR); //This is what I expect back from the server
        }

        private string SubmitCommand(string command)
        {
            //When the message is finally sent, reset certain flags
            if (_loggedOn && _messageSent)
            {
                _gotSubscriber = false;
                _gotMessage = false;
                _messageSent = false;
            }

            //If the ID= has not been prompted yet and LF was issued
            if (!_idPrompted && command == CR)
            {
                _idPrompted = true; //Logged in

                return OpenSerialConnection(); //Prompt returned
            }

            //If ID= prompted already, not logged on 
            //and shift + m (M) is issued 
            if (_idPrompted && !_loggedOn && command == ("M"))
            {
                _loggedOn = true; //Log on

                return WriteToPortAndEnter(command);
            }

            //If subscriber isn't known yet and the value is not blank or null
            if (_loggedOn && !_gotSubscriber && !string.IsNullOrEmpty(command))
            {
                _gotSubscriber = true;

                //return WriteToPortAndEnter(command, 3);
                return WriteToPort(command, 3);
            }

            return WriteToPortAndEnter(command);
        }
    }
}
