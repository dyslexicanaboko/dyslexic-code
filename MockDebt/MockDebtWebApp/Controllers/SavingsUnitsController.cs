using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using MockDebt.Data;

namespace MockDebt.WebApp.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using MockDebt.Data;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<SavingsUnit>("SavingsUnits");
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SavingsUnitsController : ODataController
    {
        private MockDebtEntities db = new MockDebtEntities();

        // GET: odata/SavingsUnits
        [EnableQuery]
        public IQueryable<SavingsUnit> GetSavingsUnits()
        {
            return db.SavingsUnits;
        }

        // GET: odata/SavingsUnits(5)
        [EnableQuery]
        public SingleResult<SavingsUnit> GetSavingsUnit([FromODataUri] int key)
        {
            return SingleResult.Create(db.SavingsUnits.Where(savingsUnit => savingsUnit.SavingsUnitId == key));
        }

        // PUT: odata/SavingsUnits(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<SavingsUnit> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SavingsUnit savingsUnit = db.SavingsUnits.Find(key);
            if (savingsUnit == null)
            {
                return NotFound();
            }

            patch.Put(savingsUnit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavingsUnitExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(savingsUnit);
        }

        // POST: odata/SavingsUnits
        public IHttpActionResult Post(SavingsUnit savingsUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SavingsUnits.Add(savingsUnit);
            db.SaveChanges();

            return Created(savingsUnit);
        }

        // PATCH: odata/SavingsUnits(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<SavingsUnit> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SavingsUnit savingsUnit = db.SavingsUnits.Find(key);
            if (savingsUnit == null)
            {
                return NotFound();
            }

            patch.Patch(savingsUnit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavingsUnitExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(savingsUnit);
        }

        // DELETE: odata/SavingsUnits(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            SavingsUnit savingsUnit = db.SavingsUnits.Find(key);
            if (savingsUnit == null)
            {
                return NotFound();
            }

            db.SavingsUnits.Remove(savingsUnit);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SavingsUnits(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.SavingsUnits.Where(m => m.SavingsUnitId == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SavingsUnitExists(int key)
        {
            return db.SavingsUnits.Count(e => e.SavingsUnitId == key) > 0;
        }
    }
}
