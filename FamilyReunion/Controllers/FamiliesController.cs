﻿using FamilyReunion.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FamilyReunion.Controllers
{
    [Authorize]
    public class FamiliesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Families
        public List<Family> GetFamilies()
        {
            return db.Families
                .Include(f => f.FamilyMembers)
                .Include("FamilyMembers.Member")
                .ToList();
        }

        // GET: api/Families/5
        [ResponseType(typeof(Family))]
        public async Task<IHttpActionResult> GetFamily(int id)
        {
            Family family = await db.Families.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            return Ok(family);
        }

        // PUT: api/Families/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFamily(int id, Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != family.FamilyId)
            {
                return BadRequest();
            }

            db.Entry(family).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(id))
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

        // POST: api/Families
        [ResponseType(typeof(Family))]
        public async Task<IHttpActionResult> PostFamily(Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Families.Add(family);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = family.FamilyId }, family);
        }

        // DELETE: api/Families/5
        [ResponseType(typeof(Family))]
        public async Task<IHttpActionResult> DeleteFamily(int id)
        {
            Family family = await db.Families.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            db.Families.Remove(family);
            await db.SaveChangesAsync();

            return Ok(family);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FamilyExists(int id)
        {
            return db.Families.Count(e => e.FamilyId == id) > 0;
        }
    }
}