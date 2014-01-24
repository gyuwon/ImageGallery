using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
            return from content in db.ImageContents
                   let comments = from comment in content.Comments
                                  select new ImageContentCommentViewModel
                                  {
                                      Id = comment.Id,
                                      ImageContentId = content.Id,
                                      UserName = comment.User.UserName,
                                      CommentText = comment.CommentText,
                                      Created = comment.Created,
                                      Updated = comment.Updated
                                  }
                   select new ImageContentViewModel
                   {
                       Id = content.Id,
                       ImageUrl = content.ImageUrl,
                       Description = content.Description,
                       UserName = content.User.UserName,
                       Created = content.Created,
                       Updated = content.Updated,
                       Comments = comments.ToList()
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
        [ResponseType(typeof(ImageContentViewModel))]
        public async Task<IHttpActionResult> PostImageContent(ImageContentBindingModel content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime now = DateTime.Now.ToUniversalTime();
            ImageContent entity = new ImageContent
            {
                UserId = this.User.Identity.GetUserId(),
                ImageUrl = content.ImageUrl,
                Description = content.Description,
                Created = now,
                Updated = now
            };

            db.ImageContents.Add(entity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = entity.Id }, new ImageContentViewModel
            {
                Id = entity.Id,
                UserName = this.User.Identity.Name,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                Created = entity.Created,
                Updated = entity.Updated
            });
        }

        // DELETE api/ImageContents/5
        [Authorize]
        public async Task<IHttpActionResult> DeleteImageContent(int id)
        {
            ImageContent content = await db.ImageContents.FindAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (content.UserId != userId)
            {
                return Unauthorized();
            }

            db.ImageContents.Remove(content);
            await db.SaveChangesAsync();

            return Ok();
        }

        // GET api/ImageContents/{id}/Comments
        [HttpGet]
        [Route("api/ImageContents/{id:int}/Comments")]
        [Queryable]
        public IQueryable<ImageContentCommentViewModel> GetComments(int id)
        {
            return from e in db.ImageContentComments
                   where e.ImageContentId == id
                   select new ImageContentCommentViewModel
                   {
                       Id = e.Id,
                       ImageContentId = e.ImageContentId,
                       UserName = e.User.UserName,
                       CommentText = e.CommentText,
                       Created = e.Created,
                       Updated = e.Updated
                   };
        }

        // GET api/ImageContents/Comments/{id}
        [HttpGet]
        [Route("api/ImageContents/Comments/{id:int}", Name = "GetImageContentComment")]
        public async Task<ImageContentCommentViewModel> GetComment(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/ImageContents/{id}/Comments
        [Authorize]
        [HttpPost]
        [Route("api/ImageContents/{id:int}/Comments")]
        [ResponseType(typeof(ImageContentCommentViewModel))]
        public async Task<IHttpActionResult> PostComment(int id, ImageContentCommentBindingModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime now = DateTime.Now.ToUniversalTime();
            ImageContentComment entity = new ImageContentComment
            {
                UserId = this.User.Identity.GetUserId(),
                ImageContentId = id,
                CommentText = comment.CommentText,
                Created = now,
                Updated = now
            };

            db.ImageContentComments.Add(entity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("GetImageContentComment", new { id = entity.Id }, new ImageContentCommentViewModel
            {
                Id = entity.Id,
                UserName = this.User.Identity.Name,
                ImageContentId = entity.ImageContentId,
                CommentText = entity.CommentText,
                Created = entity.Created,
                Updated = entity.Updated
            });
        }

        [Authorize]
        [HttpDelete]
        [Route("api/ImageContents/Comments/{id:int}")]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            ImageContentComment comment = await db.ImageContentComments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (comment.UserId != userId)
            {
                return Unauthorized();
            }

            db.ImageContentComments.Remove(comment);
            await db.SaveChangesAsync();

            return Ok();
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