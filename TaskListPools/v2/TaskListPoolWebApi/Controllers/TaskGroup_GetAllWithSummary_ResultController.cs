using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TaskListPoolLib;

namespace TaskListPoolWebApi.Controllers
{
    [RoutePrefix("api/TaskGroups")]
    public class TaskGroup_GetAllWithSummary_ResultController : ApiController
    {
        private TaskListPoolEntities db = new TaskListPoolEntities();

        // GET: api/TaskGroup_GetAllWithSummary_Result
        [Route("Summary")] //Replacement for the TaskGroupsController's "api/TaskGroup"
        public IList<TaskGroup_GetAllWithSummary_Result> GetTaskGroup_GetAllWithSummary_Result()
        {
            return db.TaskGroup_GetAllWithSummary().ToList();
            //return db.TaskGroup_GetAllWithSummary_Result;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}