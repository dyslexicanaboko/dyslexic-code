using System;
using System.Net.Mail;

namespace SimpleCmdEmail
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var a = new Args(args);

            var dtmStart = DateTime.Now;
            
            //From, To, Host, Port, Subject, Body
            Console.WriteLine($"Start @ {dtmStart}");
	
            SendEmail(a); //Works just fine
	
            var dtmEnd = DateTime.Now;

            var ts = (dtmEnd - dtmStart);

            Console.WriteLine($"End @ {dtmEnd} - {ts}");
        }

        public static void SendEmail(Args arg)
        {
            using (var client = new SmtpClient())
            {
                using (var mail = new MailMessage(arg.From, arg.To))
                {
                    client.Port = arg.Port;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Host = arg.SmtpServer;
                    
                    mail.Subject = arg.Subject;
                    mail.Body = arg.Body;
                    
                    client.Send(mail);
                }
            }
        }
    }
}
