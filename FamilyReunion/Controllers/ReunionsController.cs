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
using FamilyReunion.Models;

namespace FamilyReunion.Controllers
{
    [Authorize]
    public class ReunionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Reunions
        public IQueryable<Reunion> GetReunions()
        {
            var currentYear = DateTime.Today.Year;
            return db.Reunions
                .Where(r => r.Year > currentYear);
        }

        // GET: api/Reunions/5
        [ResponseType(typeof(Reunion))]
        public async Task<IHttpActionResult> GetReunion(int id)
        {
            Reunion reunion = await db.Reunions.FindAsync(id);
            if (reunion == null)
            {
                return NotFound();
            }

            return Ok(reunion);
        }

        // PUT: api/Reunions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReunion(int id, Reunion reunion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reunion.ReunionId)
            {
                return BadRequest();
            }

            db.Entry(reunion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReunionExists(id))
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

        // POST: api/Reunions
        [ResponseType(typeof(Reunion))]
        public async Task<IHttpActionResult> PostReunion(Reunion reunion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reunions.Add(reunion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reunion.ReunionId }, reunion);
        }

        // DELETE: api/Reunions/5
        [ResponseType(typeof(Reunion))]
        public async Task<IHttpActionResult> DeleteReunion(int id)
        {
            Reunion reunion = await db.Reunions.FindAsync(id);
            if (reunion == null)
            {
                return NotFound();
            }

            db.Reunions.Remove(reunion);
            await db.SaveChangesAsync();

            return Ok(reunion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReunionExists(int id)
        {
            return db.Reunions.Count(e => e.ReunionId == id) > 0;
        }
    }
}