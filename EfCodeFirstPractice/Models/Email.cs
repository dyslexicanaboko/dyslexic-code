using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCodeFirstPractice.Models
{
    public class Email
    {
        public int EmailId { get; set; }

        public string From { get; set; }

        public List<string> To { get; set; }

        public List<string> Cc { get; set; }

        public List<string> Bcc { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public List<Attachment> Attachments { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
