using System;
using System.Text;
using System.IO.Ports;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT;

namespace MFConsoleApplication1
{
    public static class SerialTest
    {
        public static void LoopbackTest()
        {
            using (SerialPort port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            {
                port.Handshake = Handshake.None;

                SerialPortManager spm = new SerialPortManager(port);

                string message = "Hello! @ " + DateTime.Now.ToString();

                while (true)
                {
                    //uC sends prompt
                    spm.Write("FEZ Panda II >> " + message + SerialPortManager.CRLF);

                    //User sends back message and uC repeats it
                    message = spm.Read();
                }
            }
        }

        public static void CommandsTest()
        {
            using (SerialPort port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            {
                port.Handshake = Handshake.None; //Works just fine
                //port.Handshake = Handshake.XOnXOff;

                SerialPortManager spm = new SerialPortManager(port);

                Debug.Print("======== CommandsTest ========");

                string command = null;

                while (true)
                {
                    command = spm.Read();

                    spm.TransmissionStart();

                    switch (command)
                    { 
                        case "Random Numbers":
                            string str = null;

                            foreach (int n in RandomNumbers(10))
                                str += n + SerialPortManager.CRLF;
                            
                                spm.Write(str);
                            break;
                        case "Say Hi":
                            spm.Write("Hello from FEZ Panda II @ " + DateTime.Now.ToString());
                            break;
                        case "Flash LED":
                            FlashLed(10);
                            break;
                        case "Telephone":
                            //Send nothing back on purpose
                            break;
                    }

                    spm.TransmissionEnd();
                }
            }
        }

        private static int[] RandomNumbers(int count)
        { 
            int[] arr = new int[count];

            Random r = new Random();

            for (int i = 0; i < count; i++)
            {
                arr[i] = r.Next();

                Thread.Sleep(200);
            }

            return arr;
        }

        private static void FlashLed(int flashes)
        {
            bool state = false;

            using (OutputPort led = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.LED, true))
            {

                for (int i = 0; i < flashes; i++)
                {
                    state = !state;

                    led.Write(state);

                    Thread.Sleep(300);
                }

                led.Write(true); //Keep it on
            }
        }
    }
}
