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
using task_13_2_.Models;

namespace task_13_2_.Controllers
{
    public class FactoriesController : ApiController
    {
        private ApitaskEntities db = new ApitaskEntities();

        // GET: api/Factories
        public IQueryable<Factory> GetFactories()
        {
            return db.Factories;
        }

        // GET: api/Factories/5
        [ResponseType(typeof(Factory))]
        public IHttpActionResult GetFactory(int id)
        {
            Factory factory = db.Factories.Find(id);
            if (factory == null)
            {
                return NotFound();
            }

            return Ok(factory);
        }

        // PUT: api/Factories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFactory(int id, Factory factory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factory.FactoryID)
            {
                return BadRequest();
            }

            db.Entry(factory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactoryExists(id))
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

        // POST: api/Factories
        [ResponseType(typeof(Factory))]
        public IHttpActionResult PostFactory(Factory factory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factories.Add(factory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = factory.FactoryID }, factory);
        }

        // DELETE: api/Factories/5
        [ResponseType(typeof(Factory))]
        public IHttpActionResult DeleteFactory(int id)
        {
            Factory factory = db.Factories.Find(id);
            if (factory == null)
            {
                return NotFound();
            }

            db.Factories.Remove(factory);
            db.SaveChanges();

            return Ok(factory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FactoryExists(int id)
        {
            return db.Factories.Count(e => e.FactoryID == id) > 0;
        }
    }
}