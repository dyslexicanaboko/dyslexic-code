using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCodeFirstPractice.Models
{
    public class Attachment
    {
        public int AttachmentId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
