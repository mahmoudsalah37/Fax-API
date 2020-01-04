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
    public class iamgeController : ApiController
    {
        private DBfax db = new DBfax();

        // GET: api/iamge
        public IQueryable<iamge> Getiamges()
        {
            return db.iamges;
        }

        // GET: api/iamge/5
        [ResponseType(typeof(iamge))]
        public async Task<IHttpActionResult> Getiamge(int id)
        {
            iamge iamge = await db.iamges.FindAsync(id);
            if (iamge == null)
            {
                return NotFound();
            }

            return Ok(iamge);
        }

        // PUT: api/iamge/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putiamge(int id, iamge iamge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iamge.id)
            {
                return BadRequest();
            }

            db.Entry(iamge).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!iamgeExists(id))
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

        // POST: api/iamge
        [ResponseType(typeof(iamge))]
        public async Task<IHttpActionResult> Postiamge(iamge iamge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.iamges.Add(iamge);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (iamgeExists(iamge.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = iamge.id }, iamge);
        }

        // DELETE: api/iamge/5
        [ResponseType(typeof(iamge))]
        public async Task<IHttpActionResult> Deleteiamge([FromUri]IEnumerable<int> ids)
        {
            iamge iamge = null;
            foreach (int id in ids)
            {
                iamge = await db.iamges.FindAsync(id);
                if (iamge == null)
                {
                    return NotFound();
                }

                db.iamges.Remove(iamge);
            }
            await db.SaveChangesAsync();
            return Ok(iamge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool iamgeExists(int id)
        {
            return db.iamges.Count(e => e.id == id) > 0;
        }
    }
}