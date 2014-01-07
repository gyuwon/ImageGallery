using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ImageGallery.Models;
using Microsoft.AspNet.Identity;

namespace ImageGallery.Controllers
{
    public class ImageContentsController : ApiController
    {
        private ImageGalleryContext db = new ImageGalleryContext();

        // GET api/ImageContents
        [Queryable]
        public IQueryable<ImageContentViewModel> GetImageContents()
        {
            return from e in db.ImageContents.Include(e => e.User)
                   select new ImageContentViewModel
                   {
                       Id = e.Id,
                       ImageUrl = e.ImageUrl,
                       Description = e.Description,
                       UserName = e.User.UserName
                   };
        }

        // GET api/ImageContents/5
        [ResponseType(typeof(ImageContent))]
        public async Task<IHttpActionResult> GetImageContent(int id)
        {
            ImageContent imagecontent = await db.ImageContents.FindAsync(id);
            if (imagecontent == null)
            {
                return NotFound();
            }

            return Ok(imagecontent);
        }

        // PUT api/ImageContents/5
        [Authorize]
        public async Task<IHttpActionResult> PutImageContent(int id, ImageContent content)
        {
            throw new NotImplementedException();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != content.Id)
            {
                return BadRequest();
            }

            db.Entry(content).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageContentExists(id))
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

        // POST api/ImageContents
        [Authorize]
        [ResponseType(typeof(ImageContent))]
        public async Task<IHttpActionResult> PostImageContent(ImageContent content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            content.UserId = this.User.Identity.GetUserId();

            db.ImageContents.Add(content);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = content.Id }, content);
        }

        // DELETE api/ImageContents/5
        [Authorize]
        [ResponseType(typeof(ImageContent))]
        public async Task<IHttpActionResult> DeleteImageContent(int id)
        {
            ImageContent content = await db.ImageContents.FindAsync(id);

            var userId = this.User.Identity.GetUserId();
            if (content.UserId != userId)
                return Unauthorized();

            if (content == null)
            {
                return NotFound();
            }

            db.ImageContents.Remove(content);
            await db.SaveChangesAsync();

            return Ok(content);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImageContentExists(int id)
        {
            return db.ImageContents.Count(e => e.Id == id) > 0;
        }
    }
}