using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using PaniniWS.API;
using PaniniWS.API.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PaniniWS.API.Controllers
{
    [RoutePrefix("api/Albums")]
    public class UserAlbumStickersController : ApiController
    {
        private PaniniContext db = new PaniniContext();

        // GET: api/Albums
        [Authorize]
        [HttpGet]
        [Route("")]
        public IQueryable<Album> GetAlbums()
        {
            return db.Albums;
        }

        // POST: api/Albums/5
        [Authorize]
        [HttpPost]
        [Route("{albumID:int}")]
        public IQueryable<UserAlbumSticker> GetAlbumStickers(int albumID)
        {
            string userName = ClaimsPrincipal.Current.Identity.Name;
            IdentityUser user = db.Users.Single(u => u.UserName.ToLower() == userName.ToLower());

            List<UserAlbumSticker> allStickers = new List<UserAlbumSticker>();
            allStickers = db.UserAlbumStickers.Where(uas => uas.User.Id == user.Id).Include(uas => uas.AlbumSticker.AlbumPage).ToList();
            if (allStickers.Count == 0)
            {
                List<AlbumSticker> albumStickers = db.Albums.Where(a => a.AlbumID == albumID).SelectMany(a => a.AlbumPages).SelectMany(ap => ap.AlbumStickers).ToList();
                foreach (AlbumSticker sticker in albumStickers)
                {
                    UserAlbumSticker newSticker = new UserAlbumSticker
                    {
                        User = user,
                        AlbumSticker = sticker,
                        Have = false,
                        HaveRepeated = false
                    };
                    db.UserAlbumStickers.Add(newSticker);
                }
                db.SaveChanges();

                allStickers = db.UserAlbumStickers.Where(uas => uas.User.Id == user.Id).Include(uas => uas.AlbumSticker.AlbumPage).ToList();
            }

            // Remove circular references
            allStickers.All(uas => { uas.AlbumSticker.AlbumPage.AlbumStickers.Clear(); uas.User = null; return true; });

            return allStickers.AsQueryable();
        }

        // POST: api/Albums/UpdateSticker
        [Authorize]
        [HttpPost]
        [Route("UpdateSticker")]
        public async Task<IHttpActionResult> UpdateSticker(StickerViewModel viewModel)
        {
            string userName = ClaimsPrincipal.Current.Identity.Name;

            UserAlbumSticker stickerToUpdate = db.UserAlbumStickers.SingleOrDefault(uas => uas.UserAlbumStickerID == viewModel.UserAlbumStickerID);
            if (stickerToUpdate != null)
            {
                stickerToUpdate.Have = viewModel.Have;
                stickerToUpdate.HaveRepeated = viewModel.HaveRepeated;
            }

            db.UserAlbumStickers.Attach(stickerToUpdate);
            db.Entry(stickerToUpdate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAlbumStickerExists(viewModel.UserAlbumStickerID))
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

        // GET: api/Albums/5/GetMissingStickersList
        [Authorize]
        [Route("{albumID:int}/GetMissingStickersList")]
        public IQueryable<UserAlbumSticker> GetMissingStickersList(int albumID)
        {
            string userName = ClaimsPrincipal.Current.Identity.Name;
            IdentityUser user = db.Users.Single(u => u.UserName.ToLower() == userName.ToLower());

            List<UserAlbumSticker> missingStickers = db.UserAlbumStickers.Where(uas => uas.User.Id == user.Id && uas.AlbumSticker.AlbumPage.Album.AlbumID == albumID && !uas.Have)
                                                                         .Include(uas => uas.AlbumSticker).ToList();

            // Remove circular references
            missingStickers.All(uas => { uas.User = null; return true; });

            return missingStickers.AsQueryable();
        }

        // GET: api/Albums/5/GetRepeatedStickersList
        [Authorize]
        [Route("{albumID:int}/GetRepeatedStickersList")]
        public IQueryable<UserAlbumSticker> GetRepeatedStickersList(int albumID)
        {
            string userName = ClaimsPrincipal.Current.Identity.Name;
            IdentityUser user = db.Users.Single(u => u.UserName.ToLower() == userName.ToLower());

            List<UserAlbumSticker> missingStickers = db.UserAlbumStickers.Where(uas => uas.User.Id == user.Id && uas.AlbumSticker.AlbumPage.Album.AlbumID == albumID && uas.HaveRepeated)
                                                                         .Include(uas => uas.AlbumSticker).ToList();

            // Remove circular references
            missingStickers.All(uas => { uas.User = null; return true; });

            return missingStickers.AsQueryable();
        }

        //// GET: api/UserAlbumStickers/5
        //[ResponseType(typeof(UserAlbumSticker))]
        //public async Task<IHttpActionResult> GetUserAlbumSticker(int id)
        //{
        //    UserAlbumSticker userAlbumSticker = await db.UserAlbumStickers.FindAsync(id);
        //    if (userAlbumSticker == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return Ok(userAlbumSticker);
        //}
        //
        //// PUT: api/UserAlbumStickers/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutUserAlbumSticker(int id, UserAlbumSticker userAlbumSticker)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //
        //    if (id != userAlbumSticker.UserAlbumStickerID)
        //    {
        //        return BadRequest();
        //    }
        //
        //    db.Entry(userAlbumSticker).State = EntityState.Modified;
        //
        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserAlbumStickerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //
        //    return StatusCode(HttpStatusCode.NoContent);
        //}
        //
        //// POST: api/UserAlbumStickers
        //[ResponseType(typeof(UserAlbumSticker))]
        //public async Task<IHttpActionResult> PostUserAlbumSticker(UserAlbumSticker userAlbumSticker)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //
        //    db.UserAlbumStickers.Add(userAlbumSticker);
        //    await db.SaveChangesAsync();
        //
        //    return CreatedAtRoute("DefaultApi", new { id = userAlbumSticker.UserAlbumStickerID }, userAlbumSticker);
        //}
        //
        //// DELETE: api/UserAlbumStickers/5
        //[ResponseType(typeof(UserAlbumSticker))]
        //public async Task<IHttpActionResult> DeleteUserAlbumSticker(int id)
        //{
        //    UserAlbumSticker userAlbumSticker = await db.UserAlbumStickers.FindAsync(id);
        //    if (userAlbumSticker == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    db.UserAlbumStickers.Remove(userAlbumSticker);
        //    await db.SaveChangesAsync();
        //
        //    return Ok(userAlbumSticker);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserAlbumStickerExists(int id)
        {
            return db.UserAlbumStickers.Count(e => e.UserAlbumStickerID == id) > 0;
        }
    }
}