using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NG2Sample;

namespace NG2Sample.Controllers
{
    public class tbl_heroController : ApiController
    {
        private HerodbEntities db = new HerodbEntities();

        // GET: api/tbl_hero
        public IQueryable<tbl_hero> Gettbl_hero()
        {
            return db.tbl_hero;
        }

        // GET: api/tbl_hero/5
        [ResponseType(typeof(tbl_hero))]
        public IHttpActionResult Gettbl_hero(int id)
        {
            tbl_hero tbl_hero = db.tbl_hero.Find(id);
            if (tbl_hero == null)
            {
                return NotFound();
            }

            return Ok(tbl_hero);
        }

        // PUT: api/tbl_hero/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_hero(int id, tbl_hero tbl_hero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_hero.heroId)
            {
                return BadRequest();
            }

            db.Entry(tbl_hero).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_heroExists(id))
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

        // POST: api/tbl_hero
        [ResponseType(typeof(tbl_hero))]
        public IHttpActionResult Posttbl_hero(tbl_hero tbl_hero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_hero.Add(tbl_hero);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_heroExists(tbl_hero.heroId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_hero.heroId }, tbl_hero);
        }

        // DELETE: api/tbl_hero/5
        [ResponseType(typeof(tbl_hero))]
        public IHttpActionResult Deletetbl_hero(int id)
        {
            tbl_hero tbl_hero = db.tbl_hero.Find(id);
            if (tbl_hero == null)
            {
                return NotFound();
            }

            db.tbl_hero.Remove(tbl_hero);
            db.SaveChanges();

            return Ok(tbl_hero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_heroExists(int id)
        {
            return db.tbl_hero.Count(e => e.heroId == id) > 0;
        }
    }
}