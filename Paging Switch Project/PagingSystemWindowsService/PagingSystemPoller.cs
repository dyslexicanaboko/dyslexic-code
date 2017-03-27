using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using ServerOps;
using PagingSwitchLibrary;

namespace PagingSystemWindowsService
{
    public partial class PagingSystemPoller : ServiceBase
    {
        private EventLog log = null;
        private bool _killSwitch;

        public PagingSystemPoller()
        {
            _killSwitch = false;

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogMessage("OnStart(string[] args)");

            Thread worker = new Thread(PollingFunction);

            worker.Name = "MonitorThread";
            worker.IsBackground = false;
            worker.Start();
        }

        protected override void OnStop()
        {
            _killSwitch = true;

            LogMessage("Stopping Paging System Poller");
        }

        private void PollingFunction()
        {
            bool result = false;

            log = new EventLog();
            log.Log = "Application";
            log.Source = "PagingSystemPoller.exe";

            Poller poller = new Poller(log);

            LogMessage("Starting Paging System Poller");

            result = poller.PollPagingQueue();

            LogMessage("Stopping Paging System Poller");
        }

        private void LogMessage(string msg)
        {
            ExceptionHandler.RecordNotes(msg, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\_log\\" + Utils.DateBasedIdentifier("PagingService_", Utils.Order.Prefix) + ".log");
            
            if(log != null)
                log.WriteEntry(msg);
        }
    }
}
