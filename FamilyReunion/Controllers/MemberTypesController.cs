using FamilyReunion.Models;
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
    public class MemberTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MemberTypes
        public IQueryable<MemberType> GetMemberTypes()
        {
            return db.MemberTypes;
        }

        // GET: api/MemberTypes/5
        [ResponseType(typeof(MemberType))]
        public async Task<IHttpActionResult> GetMemberType(int id)
        {
            MemberType memberType = await db.MemberTypes.FindAsync(id);
            if (memberType == null)
            {
                return NotFound();
            }

            return Ok(memberType);
        }

        // PUT: api/MemberTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMemberType(int id, MemberType memberType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != memberType.MemberTypeId)
            {
                return BadRequest();
            }

            db.Entry(memberType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberTypeExists(id))
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

        // POST: api/MemberTypes
        [ResponseType(typeof(MemberType))]
        public async Task<IHttpActionResult> PostMemberType(MemberType memberType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MemberTypes.Add(memberType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = memberType.MemberTypeId }, memberType);
        }

        // DELETE: api/MemberTypes/5
        [ResponseType(typeof(MemberType))]
        public async Task<IHttpActionResult> DeleteMemberType(int id)
        {
            MemberType memberType = await db.MemberTypes.FindAsync(id);
            if (memberType == null)
            {
                return NotFound();
            }

            db.MemberTypes.Remove(memberType);
            await db.SaveChangesAsync();

            return Ok(memberType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemberTypeExists(int id)
        {
            return db.MemberTypes.Count(e => e.MemberTypeId == id) > 0;
        }
    }
}