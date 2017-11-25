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
    public class TaskGroupLinksController : ApiController
    {
        private TaskListPoolEntities db = new TaskListPoolEntities();

        // GET: api/TaskGroupLinks
        public IQueryable<TaskGroupLink> GetTaskGroupLinks()
        {
            return db.TaskGroupLinks;
        }

        // GET: api/TaskGroupLinks/5
        [ResponseType(typeof(TaskGroupLink))]
        public IHttpActionResult GetTaskGroupLink(Guid id)
        {
            TaskGroupLink taskGroupLink = db.TaskGroupLinks.Find(id);
            if (taskGroupLink == null)
            {
                return NotFound();
            }

            return Ok(taskGroupLink);
        }

        // PUT: api/TaskGroupLinks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaskGroupLink(Guid id, TaskGroupLink taskGroupLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskGroupLink.TaskGroupLinkId)
            {
                return BadRequest();
            }

            db.Entry(taskGroupLink).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskGroupLinkExists(id))
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

        // POST: api/TaskGroupLinks
        [ResponseType(typeof(TaskGroupLink))]
        public IHttpActionResult PostTaskGroupLink(TaskGroupLink taskGroupLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskGroupLinks.Add(taskGroupLink);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TaskGroupLinkExists(taskGroupLink.TaskGroupLinkId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = taskGroupLink.TaskGroupLinkId }, taskGroupLink);
        }

        // DELETE: api/TaskGroupLinks/5
        [ResponseType(typeof(TaskGroupLink))]
        public IHttpActionResult DeleteTaskGroupLink(Guid id)
        {
            TaskGroupLink taskGroupLink = db.TaskGroupLinks.Find(id);
            if (taskGroupLink == null)
            {
                return NotFound();
            }

            db.TaskGroupLinks.Remove(taskGroupLink);
            db.SaveChanges();

            return Ok(taskGroupLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskGroupLinkExists(Guid id)
        {
            return db.TaskGroupLinks.Count(e => e.TaskGroupLinkId == id) > 0;
        }
    }
}