using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using Microsoft.SPOT;

namespace MFConsoleApplication1
{
    public class SerialPortManager
    {
        public const string CRLF = "\r\n";

        public string XON { get; private set; }
        public string XOFF { get; private set; }
        public string STX { get; private set; }
        public string ETX { get; private set; }

        public SerialPort Port { get; private set; }
        
        public string TX { get; set; }
        public string RX { get; set; }

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
            if(Port.IsOpen)
                Port.Close();
        }

        public void Write(string input)
        {
            TX = input;

            Port.Flush();
            Port.Write(Encoding.UTF8.GetBytes(TX), 0, TX.Length);

            Debug.Print("Tx: " + input);
        }

        public void TransmissionStart()
        {
            Write(STX);
        }

        public void TransmissionEnd()
        {
            Write(ETX);
        }

        public void ClearBuffers()
        {
            Port.DiscardInBuffer(); //Rx
            Port.DiscardOutBuffer(); //Tx
        }

        public string Read(int waitInSeconds = 1)
        {
            int seconds = 0;
            int minutes = 0;
            TimeSpan ts;

            try
            {
                Wait();

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

            ClearBuffers();

            return RX;
        }

        private void Wait(int seconds = 1)
        {
            for (int i = 0; i < seconds; i++)
                Thread.Sleep(1000); //1000 milliseconds = 1 second
        }
    }
}
