﻿using System;
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
    [RoutePrefix("TSDienThoais")]
    public class TSDienThoaisController : ApiController
    {
        
        private BanHangDBContext db = new BanHangDBContext();

        // GET: api/TSDienThoais
        [Route("")]
        public ICollection<TSDienThoai> GettSDienThoais()
        {
            return db.tSDienThoais.ToList();
        }

        // GET: api/TSDienThoais/5
        [Route("GetTSSanPham/{id}")]
        [ResponseType(typeof(TSDienThoai))]
        public IHttpActionResult GetTSDienThoai(string id)
        {
            TSDienThoai tSDienThoai = db.tSDienThoais.Find(id);
            if (tSDienThoai == null)
            {
                return NotFound();
            }

            return Ok(tSDienThoai);
        }

        // PUT: api/TSDienThoais/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTSDienThoai(string id, TSDienThoai tSDienThoai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tSDienThoai.IDDT)
            {
                return BadRequest();
            }

            db.Entry(tSDienThoai).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TSDienThoaiExists(id))
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

        // POST: api/TSDienThoais
        [ResponseType(typeof(TSDienThoai))]
        public IHttpActionResult PostTSDienThoai(TSDienThoai tSDienThoai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tSDienThoais.Add(tSDienThoai);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TSDienThoaiExists(tSDienThoai.IDDT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tSDienThoai.IDDT }, tSDienThoai);
        }

        // DELETE: api/TSDienThoais/5
        [ResponseType(typeof(TSDienThoai))]
        public IHttpActionResult DeleteTSDienThoai(string id)
        {
            TSDienThoai tSDienThoai = db.tSDienThoais.Find(id);
            if (tSDienThoai == null)
            {
                return NotFound();
            }

            db.tSDienThoais.Remove(tSDienThoai);
            db.SaveChanges();

            return Ok(tSDienThoai);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TSDienThoaiExists(string id)
        {
            return db.tSDienThoais.Count(e => e.IDDT == id) > 0;
        }
    }
}