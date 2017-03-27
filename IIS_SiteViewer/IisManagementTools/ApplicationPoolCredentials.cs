using Microsoft.Web.Administration;

namespace IisManagementTools
{
    public class ApplicationPoolCredential
    {
        public ApplicationPoolCredential(ApplicationPool pool)
        {
            ApplicationPoolRef = pool;
        }

        public ApplicationPoolProcessModel ProcessModel { get { return ApplicationPoolRef.ProcessModel; } }

        public ApplicationPool ApplicationPoolRef { get; private set; }

        public string ApplicationPoolName { get { return ApplicationPoolRef.Name; } }

        public string Username
        {
            get { return ProcessModel.UserName; }
            set { ProcessModel.UserName = value; }
        }

        public string Password
        {
            get { return ProcessModel.Password; }
            set { ProcessModel.Password = value; }
        }

        //public bool Equals(Creds x, Creds y)
        //{
        //    return x.Pool == y.Pool
        //        && x.User == y.User
        //        && x.Pass == y.Pass;
        //}

        //public int GetHashCode(Creds obj)
        //{
        //    return base.GetHashCode();
        //}
    }
}
