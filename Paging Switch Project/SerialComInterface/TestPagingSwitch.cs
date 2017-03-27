using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using PagingSwitchLibrary.PagingSwitch;

namespace PagingSwitchLibrary
{
    public class TestPagingSwitch : BaseMethods
    {
        public static string ENTER = "\n\r";
        public static string _response;
        public static IPagerController _server = null;
        public static StringBuilder _sb = null;

        public static void SendSampleData()
        {
            string strComOut = "No Response!";

            // Instantiate the communications
            // port with some basic settings
            SerialPort port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);

            // Open the port for communications
            port.Open();

            // Write a string
            port.Write("Hello World\n");
            
            strComOut = port.ReadLine();

            Console.WriteLine(strComOut);

            // Write a set of bytes
            //port.Write(new byte[] {0x0A, 0xE2, 0xFF}, 0, 3);

            // Close the port
            port.Close();
        }

        public static void TelnetTest()
        {
            string strComOut = "No Response!";

            // Instantiate the communications
            // port with some basic settings
            SerialPort port = new SerialPort("theshop.hopto.org:10001", 9600, Parity.None, 8, StopBits.One);

            // Open the port for communications
            port.Open();

            // Write a string
            //port.Write("Hello World\n");

            strComOut = port.ReadLine();

            Console.WriteLine(strComOut);

            // Write a set of bytes
            //port.Write(new byte[] {0x0A, 0xE2, 0xFF}, 0, 3);

            // Close the port
            port.Close();
        }

        public static string SendTestPage()
        {

            //IPagerController server = new ManualInputMethod();
            IPagerController server = new TAPInputMethod();

            if (server.ConnectToPagingSwitch())
            {
                server.SendPage("9901", "ABC");
                server.SendPage("9902", "DEF");
                server.SendPage("9903", "GHI");
                //server.SendPage("123", "ABC");
                Thread.Sleep(1000);
                
                //server.SendPage("9901", "input");
                //Thread.Sleep(1000);
                
                //server.SendPage("9901", "method");
                //Thread.Sleep(1000);

                server.CloseConnection();
            }
            else
                Console.WriteLine("Failed to connect");
            
            
            //SendPageFullTest("9901", "Message 1");
            //SendPage("9901", "Message 2");
            //return SendPage("9901", "Message 3");

            return "Finished";
        }

        //public static bool ConnectToPagingSwitch()
        //{
        //    bool success = false;
        //    //AppendResult(_sb, "CMD: " + commandAfterResponse);

        //    _response = _server.Connect().Trim();

        //    if (_response == "ID=")
        //    {
        //        _response = _server.SubmitCommand("M").Trim();

        //        if(_response == "logon granted\r\r\n\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0--- Page ---\r\n\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0Subscriber:")
        //            success = true;
        //    }

        //    return success;
        //}

        //public static void CloseConnection()
        //{
        //    _server.Close();         
        //}

        //public static string SendPage(string subscriber, string message)
        //{
        //    int intMaxMessageSize = 500;

        //    _response = "Message Not Sent";

        //    try
        //    {
        //        //Make sure there is a subscriber, if not complain
        //        if (string.IsNullOrEmpty(subscriber))
        //            return "Invalid Subscriber Provided";

        //        subscriber = subscriber.Trim();

        //        //Make sure the message isn't blank or empty
        //        if (string.IsNullOrEmpty(message))
        //            message = "blank message"; //If so fill it

        //        message = message.Trim();

        //        //Make sure that the message size isn't above 500 characters
        //        if (message.Length > intMaxMessageSize)
        //            message = message.Remove(intMaxMessageSize - 1); //If so, truncate it

        //        _server.SubmitCommand("\r").Trim();
        //        _server.SubmitCommand(subscriber).Trim();
        //        _server.SubmitCommand(message).Trim();

        //        Thread.Sleep(1000);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        //_server.Close();
        //    }

        //    return _response;
        //}

        //public static string SendPageFullTest(string subscriber, string message)
        //{
        //    int intMaxMessageSize = 500;
            
        //    _response = "Message Not Sent";

        //    try
        //    {
        //        _sb = new StringBuilder();
        //        //_server = new DummyPagerServer();
        //        _server = new ManualInputMethod();

        //        //Make sure there is a subscriber, if not complain
        //        if (string.IsNullOrEmpty(subscriber))
        //            return "Invalid Subscriber Provided";

        //        subscriber = subscriber.Trim();

        //        //Make sure the message isn't blank or empty
        //        if (string.IsNullOrEmpty(message))
        //            message = "blank message"; //If so fill it

        //        message = message.Trim();

        //        //Make sure that the message size isn't above 500 characters
        //        if (message.Length > intMaxMessageSize)
        //            message = message.Remove(intMaxMessageSize - 1); //If so, truncate it

        //        _sb.Append("Connecting...\n");

        //        _response = _server.Connect().Trim();

        //        AppendResult(_sb, _response);

        //        SendAndEvaluate("ID=", "M");
        //        SendAndEvaluate("logon granted\r\r\n\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0--- Page ---\r\n\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0Subscriber:", subscriber);

        //        AppendResult(_sb, _server.SubmitCommand("\r").Trim());
        //        AppendResult(_sb, _server.SubmitCommand("9901").Trim());
        //        AppendResult(_sb, _server.SubmitCommand("Message 1").Trim());
        //        AppendResult(_sb, "Message Sent!");

        //        Thread.Sleep(1000);

        //        AppendResult(_sb, _server.SubmitCommand("\r").Trim());
        //        AppendResult(_sb, _server.SubmitCommand("9902").Trim());
        //        AppendResult(_sb, _server.SubmitCommand("Message 2").Trim());
        //        AppendResult(_sb, "Message Sent!");

        //        Thread.Sleep(1000);

        //        AppendResult(_sb, _server.SubmitCommand("\r").Trim());
        //        AppendResult(_sb, _server.SubmitCommand("9903").Trim());
        //        AppendResult(_sb, _server.SubmitCommand("Message 3").Trim());
        //        AppendResult(_sb, "Message Sent!");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        _server.Close();
        //    }

        //    return _response;
        //}

        //public static bool SendAndEvaluate(string expectedResponse, string commandAfterResponse)
        //{
        //    bool success = false;
        //    //AppendResult(_sb, "CMD: " + commandAfterResponse);

        //    if (_response == expectedResponse)
        //    {
        //        //AppendResult(_sb, "PASS");

        //        _response = _server.SubmitCommand(commandAfterResponse).Trim();
                
        //        AppendResult(_sb, "Response: " + _response);

        //        success = true;
        //    }

        //    return success;
        //}

        public static void AppendResult(StringBuilder refSB, string strResult)
        {
            refSB.Append(strResult);
            refSB.Append("\n");

            Console.WriteLine(strResult);
        }

        public static void DummyPagerServerTest()
        {
            StringBuilder _sb = new StringBuilder();
            DummyPagerServer _server = new DummyPagerServer();

            _sb.Append(_server.Connect());
            _sb.Append("\n\n");
            _sb.Append(_server.SubmitCommand("\n"));
            _sb.Append("\n\n");
            _sb.Append(_server.SubmitCommand("M\n"));
            _sb.Append("\n\n");
            _sb.Append(_server.SubmitCommand("5503\n"));
            _sb.Append("\n\n");
            _sb.Append(_server.SubmitCommand("blah"));
            _sb.Append("\n\n");

            Console.WriteLine(_sb.ToString());
        }
    }
}
