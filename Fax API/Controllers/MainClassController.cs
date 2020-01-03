
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
    public class MainClassController : ApiController
    {
        private faxEntities db = new faxEntities();

        // GET: api/MainClass
        public IQueryable<main_class> Getmain_class()
        {
            return db.main_class;
        }

        // GET: api/MainClass/5
        [ResponseType(typeof(main_class))]
        public async Task<IHttpActionResult> Getmain_class(int id)
        {
            main_class main_class = await db.main_class.FindAsync(id);
            if (main_class == null)
            {
                return NotFound();
            }

            return Ok(main_class);
        }

        // PUT: api/MainClass/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmain_class(int id, main_class main_class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != main_class.id)
            {
                return BadRequest();
            }

            db.Entry(main_class).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!main_classExists(id))
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

        // POST: api/MainClass
        [ResponseType(typeof(main_class))]
        public async Task<IHttpActionResult> Postmain_class(main_class main_class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.main_class.Add(main_class);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = main_class.id }, main_class);
        }

        // DELETE: api/MainClass/5
        [ResponseType(typeof(main_class))]
        public async Task<IHttpActionResult> Deletemain_class(int id)
        {
            main_class main_class = await db.main_class.FindAsync(id);
            if (main_class == null)
            {
                return NotFound();
            }

            db.main_class.Remove(main_class);
            await db.SaveChangesAsync();

            return Ok(main_class);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool main_classExists(int id)
        {
            return db.main_class.Count(e => e.id == id) > 0;
        }
    }
}