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
    public class ReunionMembersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ReunionMembers
        public IQueryable<ReunionMember> GetReunionMembers()
        {
            return db.ReunionMembers;
        }

        // GET: api/ReunionMembers/5
        [ResponseType(typeof(ReunionMember))]
        public async Task<IHttpActionResult> GetReunionMember(int id)
        {
            ReunionMember reunionMember = await db.ReunionMembers.FindAsync(id);
            if (reunionMember == null)
            {
                return NotFound();
            }

            return Ok(reunionMember);
        }

        // PUT: api/ReunionMembers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReunionMember(int id, ReunionMember reunionMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reunionMember.ReunionMemberId)
            {
                return BadRequest();
            }

            db.Entry(reunionMember).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReunionMemberExists(id))
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

        // POST: api/ReunionMembers
        [ResponseType(typeof(ReunionMember))]
        public async Task<IHttpActionResult> PostReunionMember(ReunionMember reunionMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReunionMembers.Add(reunionMember);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reunionMember.ReunionMemberId }, reunionMember);
        }

        // DELETE: api/ReunionMembers/5
        [ResponseType(typeof(ReunionMember))]
        public async Task<IHttpActionResult> DeleteReunionMember(int id)
        {
            ReunionMember reunionMember = await db.ReunionMembers.FindAsync(id);
            if (reunionMember == null)
            {
                return NotFound();
            }

            db.ReunionMembers.Remove(reunionMember);
            await db.SaveChangesAsync();

            return Ok(reunionMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReunionMemberExists(int id)
        {
            return db.ReunionMembers.Count(e => e.ReunionMemberId == id) > 0;
        }
    }
}