using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerOps;

namespace PagingSwitchLibrary
{
    class ConsoleRunner
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            //SerialPortTester.SendSampleData();
            //SerialPortTester.DummyPagerServerTest();
            //TestPagingSwitch.SendTestPage();

            Poller poller = new Poller();

            poller.PollPagingQueue();

            //STX = 2
            //CR  = 13
            //ETX = 3

            //string message = Utils.GetChar(2) + "9901" + Utils.GetChar(13) + "ABC" + Utils.GetChar(13) + Utils.GetChar(3);

            //Console.WriteLine(GenerateCheckSum(message));

            Console.WriteLine("Press Any Key to Continue...");
            Console.Read();
        }

        static void CheckSum()
        {
            int total = 0;
            string nibbleA, nibbleB;
            //char[] bits = null;
            string binary = string.Empty;
            List<char> bits = null;

            //These are all of the characters in the transaction block
            List<byte> lstDecCharacters = new List<byte>();
            lstDecCharacters.Add(2);  //STX
            lstDecCharacters.Add(49); //1
            lstDecCharacters.Add(50); //2
            lstDecCharacters.Add(51); //3
            lstDecCharacters.Add(13); //CR
            lstDecCharacters.Add(65); //A
            lstDecCharacters.Add(66); //B
            lstDecCharacters.Add(67); //C
            lstDecCharacters.Add(13); //CR
            lstDecCharacters.Add(3);  //ETX

            //Sum up all of the characters' decimal values
            foreach (byte b in lstDecCharacters)
                total = total + b;

            //Convert the total to a binary number
            binary = Convert.ToString(total, 2);

            Console.WriteLine(binary);

            //Get each bit individually
            bits = new List<char>(binary.ToCharArray());
            
            //Start from the right side going left (last element first)
            bits.Reverse();

            foreach (char c in bits)
                Console.WriteLine(c);

            nibbleA = string.Format("{3}{2}{1}{0}", bits[0], bits[1], bits[2], bits[3]);
            nibbleB = string.Format("{3}{2}{1}{0}", bits[4], bits[5], bits[6], bits[7]);

            //Console.WriteLine("Dec: {0} Bin: {1}", finalResults, Convert.ToString(finalResults, 2));
            Console.WriteLine("Bin: {1} {0}", nibbleA, nibbleB);
        }

        static string GenerateCheckSum(string wholePagerMessage)
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

        static void ExampleCheckSum()
        {
            int finalResults = 0;

            List<byte> bits = new List<byte>();
            bits.Add(2);  //STX
            bits.Add(49); //1
            bits.Add(50); //2
            bits.Add(51); //3
            //bits.Add(57); //9
            //bits.Add(57); //9
            //bits.Add(48); //0
            //bits.Add(49); //1
            bits.Add(13); //CR
            bits.Add(65); //A
            bits.Add(66); //B
            bits.Add(67); //C
            bits.Add(13); //CR
            bits.Add(3);  //ETX

            finalResults += AddBits(bits);

            bits = new List<byte>();
            bits.Add(0); //STX
            bits.Add(3); //1
            bits.Add(3); //2
            bits.Add(3); //3
            bits.Add(0); //CR
            bits.Add(4); //A
            bits.Add(4); //B
            bits.Add(4); //C
            bits.Add(0); //CR
            bits.Add(0); //ETX

            finalResults += AddBits(bits);

            Console.WriteLine("Dec: {0} Bin: {1}", finalResults, Convert.ToString(finalResults, 2));
        }

        static int AddBits(List<byte> bytes)
        { 
            int total = 0;

            foreach (byte b in bytes)
                total = total + b;

            Console.WriteLine("Dec: {0} Bin: {1}", total, Convert.ToString(total, 2));

            return total;
        }
    }
}
