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
using EFWebServiceDemo;

namespace EFWebServiceDemo.Controllers
{
    public class DemoHotelsController : ApiController
    {
        private HotelContext db = new HotelContext();

        // GET: api/DemoHotels
        public IQueryable<DemoHotel> GetDemoHotels()
        {
            return db.DemoHotels;
        }

        // GET: api/DemoHotels/5
        [ResponseType(typeof(DemoHotel))]
        public IHttpActionResult GetDemoHotel(int id)
        {
            DemoHotel demoHotel = db.DemoHotels.Find(id);

            List<DemoRoom> rl = new List<DemoRoom>();
            rl.Add(new DemoRoom() { Price = 300, Types = "s" });

            demoHotel.DemoRooms = rl; //db.DemoRooms.ToList<DemoRoom>();

            if (demoHotel == null)
            {
                return NotFound();
            }

            return Ok(demoHotel);
        }

        // PUT: api/DemoHotels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDemoHotel(int id, DemoHotel demoHotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != demoHotel.Hotel_No)
            {
                return BadRequest();
            }

            db.Entry(demoHotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemoHotelExists(id))
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

        // POST: api/DemoHotels
        [ResponseType(typeof(DemoHotel))]
        public IHttpActionResult PostDemoHotel(DemoHotel demoHotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DemoHotels.Add(demoHotel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DemoHotelExists(demoHotel.Hotel_No))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = demoHotel.Hotel_No }, demoHotel);
        }

        // DELETE: api/DemoHotels/5
        [ResponseType(typeof(DemoHotel))]
        public IHttpActionResult DeleteDemoHotel(int id)
        {
            DemoHotel demoHotel = db.DemoHotels.Find(id);
            if (demoHotel == null)
            {
                return NotFound();
            }

            db.DemoHotels.Remove(demoHotel);
            db.SaveChanges();

            return Ok(demoHotel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DemoHotelExists(int id)
        {
            return db.DemoHotels.Count(e => e.Hotel_No == id) > 0;
        }
    }
}