using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskListPoolLib
{
    public partial class TaskGroup_GetAllWithSummary_Result
    {
        /// <summary>
        /// This is an ID that is required by EF to be here
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int TaskCount { get { return Tasks.GetValueOrDefault(); } }
    }
}
