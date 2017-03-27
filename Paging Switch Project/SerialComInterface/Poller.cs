using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using ServerOps;
using PagingSwitchLibrary.PagingSwitch;
using PagingSwitchLibrary.BusinessObjects;
using PagingSwitchLibrary.DataAccess;

namespace PagingSwitchLibrary
{
    public class Poller : BaseMethods
    {
        private EventLog _logRef;
        private bool _killSwitch;
        private bool _debug;

        public Poller()
        {
            _killSwitch = false;
            _logRef = null;
        }

        public Poller(EventLog eventLogReference)
        {
            _killSwitch = false;
            _logRef = eventLogReference;
        }

        public bool PollPagingQueue()
        {
            bool connected = false,
                 updated = false;

            string strSubscriber = string.Empty,
                   strMessage = string.Empty,
                   strResponse = string.Empty;

            IPagerController server = null;
            DataTable dt = null;

            WriteToLog("Entering Main Loop");

            try
            {
                server = new TAPInputMethod();

                PagingQueueDAL.Initialize();

                _debug = Config.Debug;

                //While this loop is not killed (kill switch == false)
                while (!_killSwitch)
                {
                    //Poll the Queue
                    dt = PagingQueue.GetUnsentMessages();
                    
                    //Don't bother if there are no rows
                    if (dt.Rows.Count > 0)
                    {
                        Debug(string.Format("Messages: {0}", dt.Rows.Count));
                        
                        connected = server.ConnectToPagingSwitch();

                        Debug(string.Format("Connected: {0}", connected));

                        //If a connection couldn't be established
                        if (!connected)
                        {
                            _killSwitch = false;

                            WriteToLog("Could not connect to paging switch. Stopping Poller.");

                            break;
                        }

                        foreach (DataRow dr in dt.Rows)
                        {
                            strSubscriber = dr["SubscriberID"].ToString();
                            strMessage = dr["MessageText"].ToString();

                            strResponse = server.SendPage(strSubscriber, strMessage);

                            //Make sure that the ACK character is returned
                            //if (strResponse.Contains(Utils.GetChar(6)))
                            //This isn't completely working yet
                            {
                                dr["ResponseText"] = strResponse;
                                dr["IsSent"] = 1;
                                dr["DateTimeSent"] = DateTime.Now;
                            }

                            Debug(string.Format("Response: {0}", strResponse));
                        }

                        Debug("Updating");

                        //Update these messages in the queue
                        updated = PagingQueueDAL.UpdatePagingQueue(dt);
                        
                        Debug(string.Format("Updated: {0}", updated));

                        Debug("Closing Connection");

                        server.CloseConnection();
                    }

                    //Poll the Queue every 5 Seconds
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);

                WriteToLog("Stopping Service. Exception Occured In Main Loop. Exception = " + ex.ToString());
            }

            return _killSwitch;
        }

        private void WriteToLog(string message)
        {
            LogNotes(message);

            if (_logRef != null)
                _logRef.WriteEntry(message);

            Console.WriteLine(message);
        }

        private void Debug(string message)
        {
            if(_debug)
                WriteToLog(message);
        }
    }
}
