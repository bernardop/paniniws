using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaniniWS.API.Models
{
    public class UserAlbumSticker
    {
        public int UserAlbumStickerID { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual AlbumSticker AlbumSticker { get; set; }
        public bool Have { get; set; }
        public bool HaveRepeated { get; set; }
    }
}