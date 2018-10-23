using System;

namespace SimpleCmdEmail
{
    public class Args
    {
        public Args(string[] args)
        {
            SmtpServer = args[0];
            Port = Convert.ToInt32(args[1]);
            From = args[2];
            To = args[3];
            Subject = args[4];
            Body = args[5];
        }

        public string SmtpServer { get; set; }

        public int Port { get; set; }

        public string From { get; set; }
            
        public string To { get; set; }
            
        public string Subject { get; set; }
            
        public string Body { get; set; }
    }
}
