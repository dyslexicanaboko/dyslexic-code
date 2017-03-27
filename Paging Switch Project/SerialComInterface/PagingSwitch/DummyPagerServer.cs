using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagingSwitchLibrary.PagingSwitch
{
    public class DummyPagerServer : PagerControllerBase, IPagerController 
    {
        /* Test Instructions:
         * 1. \n
         * 2. "ID="
         * 3. M + \n
         * 4. "logon granted"
         *    "--- Page ---" 
         *    "Subscriber: "
         * 5. 5503 + \n
         * 6. "Message [500]: "
         * 7. what ever + \n
         * 8. "transaction 1 acknowledge"
         */
        public DummyPagerServer()
        { 
        
        }

        public string Connect()
        {
            //Send that first <LF> here
            return SubmitCommand(LF); //This is what I expect back from the server
        }

        public string SubmitCommand(string command)
        {
            //When the message is finally sent, reset certain flags
            if (_loggedOn && _messageSent)
            { 
                _gotSubscriber = false;
                _gotMessage = false;
                _messageSent = false;
            }

            //If the ID= has not been prompted yet and ENTER was issued
            if (!_idPrompted && command == LF)
            {
                _idPrompted = true; //Logged in
                return "ID="; //Prompt returned
            }

            //If ID= prompted already, not logged on 
            //and shift + M is issued 
            if (_idPrompted && !_loggedOn && command == "M\n")
            {
                _loggedOn = true; //Log on

                //Return log on script and ask for subscriber
                return "logon granted\n" +
                       "--- Page ---\n" +
                       "Subscriber: ";
            }

            //If subscriber isn't known yet and the value is not blank or null
            if (_loggedOn && !_gotSubscriber && !string.IsNullOrEmpty(command))
            {
                _gotSubscriber = true;

                return "Message [500]: ";
            }

            //If logged on and we have no got or sent the message yet
            if (_loggedOn && !_gotMessage && command != null)
            {
                _gotMessage = true;
                _messageSent = true;

                return "transaction 1 acknowledge";
            }

            return string.Empty;
        }

        public void Close()
        { 
            //Just for interface, doesn't do anything cause it is a dummy
        }

        #region IPagerController Members


        public bool ConnectToPagingSwitch()
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public string SendPage(string subscriber, string message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
