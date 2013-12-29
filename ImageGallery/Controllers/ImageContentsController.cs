using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ImageGallery.Models;

namespace ImageGallery.Controllers
{
    public class ImageContentsController : ApiController
    {
        private ImageGalleryContext db = new ImageGalleryContext();

        // GET api/ImageContents
        [Queryable]
        public IQueryable<ImageContent> GetImageContents()
        {
            return db.ImageContents;
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
        public async Task<IHttpActionResult> PutImageContent(int id, ImageContent imagecontent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imagecontent.Id)
            {
                return BadRequest();
            }

            db.Entry(imagecontent).State = EntityState.Modified;

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
        public async Task<IHttpActionResult> PostImageContent(ImageContent imagecontent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ImageContents.Add(imagecontent);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = imagecontent.Id }, imagecontent);
        }

        // DELETE api/ImageContents/5
        [Authorize]
        [ResponseType(typeof(ImageContent))]
        public async Task<IHttpActionResult> DeleteImageContent(int id)
        {
            ImageContent imagecontent = await db.ImageContents.FindAsync(id);
            if (imagecontent == null)
            {
                return NotFound();
            }

            db.ImageContents.Remove(imagecontent);
            await db.SaveChangesAsync();

            return Ok(imagecontent);
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