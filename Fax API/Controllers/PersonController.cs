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
    public class PersonController : ApiController
    {
        private faxEntities db = new faxEntities();

        // GET: api/Person
        public IQueryable<person> Getpeople()
        {
            return db.people;
        }

        // GET: api/Person/5
        [ResponseType(typeof(person))]
        public async Task<IHttpActionResult> Getperson(int id)
        {
            person person = await db.people.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/Person/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putperson(int id, person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.id)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!personExists(id))
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

        // POST: api/Person
        [ResponseType(typeof(person))]
        public async Task<IHttpActionResult> Postperson(person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.people.Add(person);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = person.id }, person);
        }

        // DELETE: api/Person/5
        [ResponseType(typeof(person))]
        public async Task<IHttpActionResult> Deleteperson(int id)
        {
            person person = await db.people.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.people.Remove(person);
            await db.SaveChangesAsync();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool personExists(int id)
        {
            return db.people.Count(e => e.id == id) > 0;
        }
    }
}