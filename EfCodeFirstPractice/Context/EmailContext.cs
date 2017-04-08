using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EfCodeFirstPractice.Models;

namespace EfCodeFirstPractice.Context
{
    public class EmailContext : DbContext
    {
        public DbSet<Email> Email { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
    }
}
