using Microsoft.AspNet.Identity.EntityFramework;
using PaniniWS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PaniniWS
{
    public class PaniniContext : IdentityDbContext<IdentityUser>
    {
        public PaniniContext()
            : base("PaniniContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumPage> AlbumPages { get; set; }
        public DbSet<AlbumSticker> AlbumSticker { get; set; }
        public DbSet<UserAlbumSticker> UserAlbumStickers { get; set; }
    }
}