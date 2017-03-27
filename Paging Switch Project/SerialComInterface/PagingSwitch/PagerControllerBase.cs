using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using ServerOps;
using PagingSwitchLibrary.BusinessObjects;

namespace PagingSwitchLibrary.PagingSwitch
{
    public abstract class PagerControllerBase : BaseMethods
    {
        public PagerControllerBase()
        {
            _port = new SerialPort(Config.SerialPortName, 1200, Parity.Even, 7, StopBits.One);

            _stx = Utils.GetChar(2); //2 - Start of Text
            _etx = Utils.GetChar(3); //3 - End of Text
            _eot = Utils.GetChar(4); //4 - End of Transmission
            _ack = Utils.GetChar(6); //6 - Acknowledge
            _lf  = Utils.GetChar(10); //10 - Line Feed (\n)
            _cr  = Utils.GetChar(13); //13 - Carraige Return (\r)
            _esc = Utils.GetChar(27); //27 - Escape
        }

        #region Character Constants
        private string _stx; //2 - Start of Text
        protected string STX
        {
            get {return _stx;}
        }
        
        private string _etx; //3 - End of Text
        protected string ETX
        {
            get { return _etx; }
        }        
        
        private string _eot; //4 - End of Transmission
        protected string EOT
        {
            get { return _eot; }
        }        
        
        private string _ack; //6 - Acknowledge
        protected string ACK
        {
            get { return _ack; }
        }        
        
        private string _lf;  //10 - Line Feed
        protected string LF
        {
            get { return _lf; }
        }        
        
        private string _cr;  //13 - Carraige Return
        protected string CR
        {
            get { return _cr; }
        }        
        
        private string _esc; //27 - Escape
        protected string ESC
        {
            get { return _esc; }
        }
        #endregion

        private SerialPort _port = null;

        protected bool _idPrompted = false;
        protected bool _loggedOn = false;
        protected bool _gotSubscriber = false;
        protected bool _gotMessage = false;
        protected bool _messageSent = false;
        protected string TX;
        protected string RX;

        protected string OpenSerialConnection()
        {
            _port.Open();
            _port.Write(_cr);

            return ReadFromPort();
        }

        protected string WriteToPortAndEnter(string strToSerialPort)
        {
            return WriteToPortAndEnter(strToSerialPort, 1);
        }

        protected string WriteToPortAndEnter(string strToSerialPort, int secWait)
        {
            return WriteToPort(strToSerialPort + _cr, secWait);
        }

        protected string WriteToPort(string strToSerialPort, int secWait)
        {
            TX = strToSerialPort;

            foreach (char c in strToSerialPort)
                _port.Write(c.ToString());

            return ReadFromPort(secWait);
        }

        protected string ReadFromPort()
        {
            return ReadFromPort(1);
        }

        protected string ReadFromPort(int secWait)
        {
            char[] arrChar = null;

            try
            {
                while (_port.BytesToRead <= 0)
                    WaitForRead(secWait);

                Console.WriteLine();

                arrChar = new char[_port.BytesToRead];

                _port.Read(arrChar, 0, _port.BytesToRead);

                RX = new string(arrChar);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return RX;
        }

        protected void WaitForRead(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
        }

        protected void Close()
        {
            if (_port != null)
                _port.Close();
        }

        //protected string 
    }
}
