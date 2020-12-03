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
using WebBanHangAPI.DataContextLayer;
using WebBanHangAPI.Models;

namespace WebBanHangAPI.Controllers
{
    [RoutePrefix("TSDongHos")]
    public class TSDongHoesController : ApiController
    {
        private BanHangDBContext db = new BanHangDBContext();

        // GET: api/TSDongHoes
        [Route("")]
        public ICollection<TSDongHo> GettSDongHos()
        {
            return db.tSDongHos.ToList();
        }

        // GET: api/TSDongHoes/5
        [Route("GetTSSanPham/{id}")]
        [ResponseType(typeof(TSDongHo))]
        public IHttpActionResult GetTSDongHo(string id)
        {
            TSDongHo tSDongHo = db.tSDongHos.Find(id);
            if (tSDongHo == null)
            {
                return NotFound();
            }

            return Ok(tSDongHo);
        }

        // PUT: api/TSDongHoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTSDongHo(string id, TSDongHo tSDongHo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tSDongHo.IDDH)
            {
                return BadRequest();
            }

            db.Entry(tSDongHo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TSDongHoExists(id))
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

        // POST: api/TSDongHoes
        [ResponseType(typeof(TSDongHo))]
        public IHttpActionResult PostTSDongHo(TSDongHo tSDongHo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tSDongHos.Add(tSDongHo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TSDongHoExists(tSDongHo.IDDH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tSDongHo.IDDH }, tSDongHo);
        }

        // DELETE: api/TSDongHoes/5
        [ResponseType(typeof(TSDongHo))]
        public IHttpActionResult DeleteTSDongHo(string id)
        {
            TSDongHo tSDongHo = db.tSDongHos.Find(id);
            if (tSDongHo == null)
            {
                return NotFound();
            }

            db.tSDongHos.Remove(tSDongHo);
            db.SaveChanges();

            return Ok(tSDongHo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TSDongHoExists(string id)
        {
            return db.tSDongHos.Count(e => e.IDDH == id) > 0;
        }
    }
}