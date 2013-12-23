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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ImageGallery.Models;

namespace ImageGallery.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using ImageGallery.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ImageContent>("ImageContents");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class ImageContentsController : ODataController
    {
        private ImageGalleryContext db = new ImageGalleryContext();

        // GET odata/ImageContents
        [Queryable]
        public IQueryable<ImageContent> GetImageContents()
        {
            return db.ImageContents;
        }

        // GET odata/ImageContents(5)
        [Queryable]
        public SingleResult<ImageContent> GetImageContent([FromODataUri] int key)
        {
            return SingleResult.Create(db.ImageContents.Where(imagecontent => imagecontent.Id == key));
        }

        // PUT odata/ImageContents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, ImageContent imagecontent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != imagecontent.Id)
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
                if (!ImageContentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(imagecontent);
        }

        // POST odata/ImageContents
        public async Task<IHttpActionResult> Post(ImageContent imagecontent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ImageContents.Add(imagecontent);
            await db.SaveChangesAsync();

            return Created(imagecontent);
        }

        // PATCH odata/ImageContents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ImageContent> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageContent imagecontent = await db.ImageContents.FindAsync(key);
            if (imagecontent == null)
            {
                return NotFound();
            }

            patch.Patch(imagecontent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageContentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(imagecontent);
        }

        // DELETE odata/ImageContents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ImageContent imagecontent = await db.ImageContents.FindAsync(key);
            if (imagecontent == null)
            {
                return NotFound();
            }

            db.ImageContents.Remove(imagecontent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImageContentExists(int key)
        {
            return db.ImageContents.Count(e => e.Id == key) > 0;
        }
    }
}
