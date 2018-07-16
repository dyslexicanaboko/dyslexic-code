using System;
using System.Text;
using System.IO.Ports;

namespace MicroControllerConsole
{
    public static class Communication
    {
        public static void SendCommands()
        {
            //case "Random Numbers":
            //    foreach (int n in RandomNumbers(10))
            //        spm.Write(n.ToString());
            //    break;
            //case "Say Hi":
            //    spm.Write("Hello from FEZ Panda II @ " + DateTime.Now.ToString());
            //    break;
            //case "Flash LED":
            //    FlashLed(10);
            //    break;

            using (SerialPort port = new SerialPort("COM15", 9600, Parity.None, 8, StopBits.One))
            {
                port.Handshake = Handshake.None; //This works just fine
                //port.Handshake = Handshake.XOnXOff;

                SerialPortManager spm = new SerialPortManager(port);

                //spm.Conversation("Telephone");
                //spm.Conversation("Say Hi", 5);
                //spm.Conversation("Random Numbers");
                spm.Write("Flash LED");
            }
        }
    }
}
