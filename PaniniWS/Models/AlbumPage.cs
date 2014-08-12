using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaniniWS.API.Models
{
    public class AlbumPage
    {
        public int AlbumPageID { get; set; }
        public virtual Album Album { get; set; }
        public int PageNumber { get; set; }
        public string Description { get; set; }
        public string PageImageFilePath { get; set; }

        public virtual ICollection<AlbumSticker> AlbumStickers { get; set; }
    }
}