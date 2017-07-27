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
    public class FamilyMembersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FamilyMembers
        public IQueryable<FamilyMember> GetFamilyMembers()
        {
            return db.FamilyMembers;
        }

        // GET: api/FamilyMembers/5
        [ResponseType(typeof(FamilyMember))]
        public async Task<IHttpActionResult> GetFamilyMember(int id)
        {
            FamilyMember familyMember = await db.FamilyMembers.FindAsync(id);
            if (familyMember == null)
            {
                return NotFound();
            }

            return Ok(familyMember);
        }

        // PUT: api/FamilyMembers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFamilyMember(int id, FamilyMember familyMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != familyMember.FamilyMemberId)
            {
                return BadRequest();
            }

            db.Entry(familyMember).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyMemberExists(id))
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

        // POST: api/FamilyMembers
        [ResponseType(typeof(FamilyMember))]
        public async Task<IHttpActionResult> PostFamilyMember(FamilyMember familyMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FamilyMembers.Add(familyMember);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = familyMember.FamilyMemberId }, familyMember);
        }

        // DELETE: api/FamilyMembers/5
        [ResponseType(typeof(FamilyMember))]
        public async Task<IHttpActionResult> DeleteFamilyMember(int id)
        {
            FamilyMember familyMember = await db.FamilyMembers.FindAsync(id);
            if (familyMember == null)
            {
                return NotFound();
            }

            db.FamilyMembers.Remove(familyMember);
            await db.SaveChangesAsync();

            return Ok(familyMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FamilyMemberExists(int id)
        {
            return db.FamilyMembers.Count(e => e.FamilyMemberId == id) > 0;
        }
    }
}