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
    public class RankController : ApiController
    {
        private DBfax db = new DBfax();

        // GET: api/Rank
        public IQueryable<rank> Getranks()
        {
            return db.ranks;
        }

        // GET: api/Rank/5
        [ResponseType(typeof(rank))]
        public async Task<IHttpActionResult> Getrank(int id)
        {
            rank rank = await db.ranks.FindAsync(id);
            if (rank == null)
            {
                return NotFound();
            }

            return Ok(rank);
        }

        // PUT: api/Rank/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putrank(int id, rank rank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rank.id)
            {
                return BadRequest();
            }

            db.Entry(rank).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!rankExists(id))
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

        // POST: api/Rank
        [ResponseType(typeof(rank))]
        public async Task<IHttpActionResult> Postrank(rank rank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ranks.Add(rank);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rank.id }, rank);
        }

        // DELETE: api/Rank/5
        [ResponseType(typeof(rank))]
        public async Task<IHttpActionResult> Deleterank([FromUri]IEnumerable<int> ids)
        {
            rank rank = null;
            foreach (int id in ids)
            {
                rank = await db.ranks.FindAsync(id);
                if (rank == null)
                {
                    return NotFound();
                }

                db.ranks.Remove(rank);
            }
            await db.SaveChangesAsync();
            return Ok(rank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool rankExists(int id)
        {
            return db.ranks.Count(e => e.id == id) > 0;
        }
    }
}