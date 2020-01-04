using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Fax_API.Models;

namespace Fax_API.Controllers
{
    public class SubClassController : ApiController
    {
        private DBfax db = new DBfax();

        // GET: api/SubClass
        public IQueryable<sub_class> Getsub_class()
        {
            return db.sub_class;
        }

        // GET: api/SubClass/5
        [ResponseType(typeof(sub_class))]
        public async Task<IHttpActionResult> Getsub_class(int id)
        {
            sub_class sub_class = await db.sub_class.FindAsync(id);
            if (sub_class == null)
            {
                return NotFound();
            }

            return Ok(sub_class);
        }

        // PUT: api/SubClass/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putsub_class(int id, sub_class sub_class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sub_class.id)
            {
                return BadRequest();
            }

            db.Entry(sub_class).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sub_classExists(id))
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

        // POST: api/SubClass
        [ResponseType(typeof(sub_class))]
        public async Task<IHttpActionResult> Postsub_class(sub_class sub_class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sub_class.Add(sub_class);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sub_class.id }, sub_class);
        }

        // DELETE: api/SubClass/5
        [ResponseType(typeof(sub_class))]
        public async Task<IHttpActionResult> Deletesub_class([FromUri]IEnumerable<int> ids)
        {
            sub_class sub_class = null;
            foreach (int id in ids)
            {
                sub_class = await db.sub_class.FindAsync(id);
                if (sub_class == null)
                {
                    return NotFound();
                }

                db.sub_class.Remove(sub_class);
            }
            await db.SaveChangesAsync();
            return Ok(sub_class);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sub_classExists(int id)
        {
            return db.sub_class.Count(e => e.id == id) > 0;
        }
    }
}