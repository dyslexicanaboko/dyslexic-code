using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace PagingSystemWindowsService
{
    [RunInstaller(true)]
    public partial class PagingServiceInstaller : Installer
    {
        public PagingServiceInstaller()
        {
            InitializeComponent();

            ServiceInstaller serviceInstaller = new ServiceInstaller();
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information
            serviceInstaller.DisplayName = "Paging System Poller Windows Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            //# This must be identical to the WindowsService.ServiceBase name
            //# set in the constructor of WindowsService.cs
            serviceInstaller.ServiceName = "PagingSystemPoller";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
