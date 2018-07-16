using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestingGroundsCS
{
    public class ThreadsTest
    {
        public volatile bool IsTimmyFinished;
        public TextBox txtBoxReference;
        public Button btnStartButtonReference;
        public int SleepTime = 5;
        private BackgroundWorker backgroundWorker1;

        public ThreadsTest()
        { 
            IsTimmyFinished = false;
        }

        private void DummyMethod()
        { 
            //Doesn't do anything
        }

        public void ThreadStubMethod()
        {
            ThreadClassWork(SleepTime);
        }

        public void ThreadClassWork(int workForInSeconds)
        {
            IsTimmyFinished = false;

            try
            {
                WriteLine("Timmah!!! Hajime!");

                for (int i = 0; i < workForInSeconds; i++)
                {
                    //Console.WriteLine("Child Sleeping: {0}", (i + 1));
                    WriteLine(string.Format("Child Sleeping: {0}", (i + 1)));
                    Thread.Sleep(1000);
                }

                WriteLine("Timmy is finished!");

                ReEnableButton();
            }
            catch (Exception ex)
            {
                IsTimmyFinished = true;
                throw;
            }
            
            IsTimmyFinished = true;
        }

        private void Write(string message)
        {
            txtBoxReference.Text += message;
        }

        delegate void SetTextCallback(string text);

        private void WriteLine(string message)
        {
            try
            {
                if (this.txtBoxReference.InvokeRequired)
                    txtBoxReference.Invoke(new SetTextCallback(WriteLine), new object[] { message });
                else
                    txtBoxReference.Text += message + "\r\n";
            }
            catch (Exception ex)
            {
                IsTimmyFinished = true;
                throw;
            }
        }

        delegate void EnableStartButton();

        private void ReEnableButton()
        {
            if (btnStartButtonReference.InvokeRequired)
                btnStartButtonReference.Invoke(new EnableStartButton(ReEnableButton));
            else
                btnStartButtonReference.Enabled = true;
        }
    }
}
