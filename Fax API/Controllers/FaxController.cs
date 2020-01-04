using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Fax_API.Models;

namespace Fax_API.Controllers
{
    public class FaxController : ApiController
    {
        private DBfax db = new DBfax();

        // GET: api/Fax
        public IQueryable<fax> Getfaxes()
        {
            return db.faxes;
        }

        // GET: api/Fax/5
        [ResponseType(typeof(fax))]
        public async Task<IHttpActionResult> Getfax(int id)
        {
            fax fax = await db.faxes.FindAsync(id);
            if (fax == null)
            {
                return NotFound();
            }

            return Ok(fax);
        }

        // PUT: api/Fax/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putfax(int id, fax fax)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fax.id)
            {
                return BadRequest();
            }

            db.Entry(fax).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!faxExists(id))
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

        // POST: api/Fax
        [ResponseType(typeof(fax))]
        public async Task<IHttpActionResult> Postfax(fax fax)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.faxes.Add(fax);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (faxExists(fax.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = fax.id }, fax);
        }

        // DELETE: api/Fax/5
        [ResponseType(typeof(fax))]
        public async Task<IHttpActionResult> Deletefax([FromUri]IEnumerable<int> ids)
        {
            fax fax = null;
            foreach (int id in ids)
            {
                fax = await db.faxes.FindAsync(id);
                if (fax == null)
                {
                    return NotFound();
                }

                db.faxes.Remove(fax);
            }
            await db.SaveChangesAsync();
            return Ok(fax);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool faxExists(int id)
        {
            return db.faxes.Count(e => e.id == id) > 0;
        }
    }
}