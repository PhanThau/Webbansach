using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nhom7_WebsiteClothes.Models;

namespace Nhom7_WebsiteClothes.Controllers
{
    public class ClothesAPIController : ApiController
    {
        private ClothesModelContext db = new ClothesModelContext();

        public IQueryable<Cloth> GetBooks()
        {
            return db.Clothes;
        }

        public IHttpActionResult GetBook(int id)
        {
            Cloth book = db.Clothes.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IHttpActionResult PostBook(Cloth book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clothes.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        [HttpPut]
        public IHttpActionResult PutBook(int id, Cloth book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            Cloth book = db.Clothes.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Clothes.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        private bool BookExists(int id)
        {
            return db.Clothes.Count(e => e.Id == id) > 0;
        }
    }
}
