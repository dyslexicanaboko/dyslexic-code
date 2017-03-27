using Microsoft.Web.Administration;

namespace IisManagementTools
{
    public class ApplicationPoolInfo
    {
        private ApplicationPool _appPoolRef;

        public ApplicationPoolInfo(ApplicationPool applicationPool)
        {
            _appPoolRef = applicationPool;

            var a = applicationPool;

            ProcessId = GetWorkerProcessId();
            Name = a.Name;
            PipelineMode = a.ManagedPipelineMode;
            IdentityType = a.ProcessModel.IdentityType;
            IdentityUser = GetApplicationPoolUser();
            Is32Bit = a.Enable32BitAppOnWin64;
            State = a.State;
        }

        public string Name { get; set; }

        public ManagedPipelineMode PipelineMode { get; set; }

        public ProcessModelIdentityType IdentityType { get; set; }

        public string IdentityUser { get; set; }

        public bool Is32Bit { get; set; }

        public ObjectState State { get; set; }

        public int ProcessId { get; internal set; }
        
        public int GetWorkerProcessId()
        {
            WorkerProcessCollection wpc = _appPoolRef.WorkerProcesses;

            return wpc == null || wpc.Count == 0 ? 0 : wpc[0].ProcessId;
        }

        public string GetApplicationPoolUser()
        {
            var m = _appPoolRef.ProcessModel;

            return m.IdentityType == ProcessModelIdentityType.SpecificUser ? m.UserName : null;
        }
    }
}
