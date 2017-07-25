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
    public class TaskGroupsController : ApiController
    {
        private TaskListPoolEntities db = new TaskListPoolEntities();

        // GET: api/TaskGroups
        public IQueryable<TaskGroup> GetTaskGroups()
        {
            return db.TaskGroups;
        }

        // GET: api/TaskGroups/5
        [ResponseType(typeof(TaskGroup))]
        public IHttpActionResult GetTaskGroup(int id)
        {
            TaskGroup taskGroup = db.TaskGroups.Find(id);

            //taskGroup.Tasks = db.Tasks.Find(taskGroup.TaskGroupLinks.Select(x => x.)

            if (taskGroup == null)
            {
                return NotFound();
            }

            return Ok(taskGroup);
        }

        // PUT: api/TaskGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaskGroup(int id, TaskGroup taskGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskGroup.TaskGroupId)
            {
                return BadRequest();
            }

            db.Entry(taskGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskGroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TaskGroups
        [ResponseType(typeof(TaskGroup))]
        public IHttpActionResult PostTaskGroup(TaskGroup taskGroup)
        {
            taskGroup.CreatedOn = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.TaskGroups.Add(taskGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = taskGroup.TaskGroupId }, taskGroup);
        }

        // DELETE: api/TaskGroups/5
        [ResponseType(typeof(TaskGroup))]
        public IHttpActionResult DeleteTaskGroup(int id)
        {
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            if (taskGroup == null)
            {
                return NotFound();
            }

            db.TaskGroups.Remove(taskGroup);
            db.SaveChanges();

            return Ok(taskGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskGroupExists(int id)
        {
            return db.TaskGroups.Count(e => e.TaskGroupId == id) > 0;
        }
    }
}