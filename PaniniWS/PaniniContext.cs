using Microsoft.AspNet.Identity.EntityFramework;
using PaniniWS.API.Entities;
using PaniniWS.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PaniniWS.API
{
    public class PaniniContext : IdentityDbContext<IdentityUser>
    {
        public PaniniContext()
            : base("PaniniContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumPage> AlbumPages { get; set; }
        public DbSet<AlbumSticker> AlbumSticker { get; set; }
        public DbSet<UserAlbumSticker> UserAlbumStickers { get; set; }
    }
}