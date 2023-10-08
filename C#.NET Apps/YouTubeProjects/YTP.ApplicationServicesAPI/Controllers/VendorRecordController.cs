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
using YTP.ApplicationServicesAPI.Models;

namespace YTP.ApplicationServicesAPI.Controllers
{
    public class VendorRecordController : ApiController
    {
        private AppServiceModel db = new AppServiceModel();

        // GET: api/VendorRecord
        public IQueryable<VendorRecord> GetVendorRecords()
        {
            return db.VendorRecords;
        }

        // GET: api/VendorRecord/5
        [ResponseType(typeof(VendorRecord))]
        public IHttpActionResult GetVendorRecord(int id)
        {
            VendorRecord vendorRecord = db.VendorRecords.Find(id);
            if (vendorRecord == null)
            {
                return NotFound();
            }

            return Ok(vendorRecord);
        }

        // PUT: api/VendorRecord/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendorRecord(int id, VendorRecord vendorRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorRecord.VendorId)
            {
                return BadRequest();
            }

            db.Entry(vendorRecord).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorRecordExists(id))
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

        // POST: api/VendorRecord
        [ResponseType(typeof(VendorRecord))]
        public IHttpActionResult PostVendorRecord(VendorRecord vendorRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendorRecords.Add(vendorRecord);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendorRecord.VendorId }, vendorRecord);
        }

        // DELETE: api/VendorRecord/5
        [ResponseType(typeof(VendorRecord))]
        public IHttpActionResult DeleteVendorRecord(int id)
        {
            VendorRecord vendorRecord = db.VendorRecords.Find(id);
            if (vendorRecord == null)
            {
                return NotFound();
            }

            db.VendorRecords.Remove(vendorRecord);
            db.SaveChanges();

            return Ok(vendorRecord);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorRecordExists(int id)
        {
            return db.VendorRecords.Count(e => e.VendorId == id) > 0;
        }
    }
}