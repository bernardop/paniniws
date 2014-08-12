using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaniniWS.API.Models
{
    public class StickerViewModel
    {
        public int UserAlbumStickerID { get; set; }
        public bool Have { get; set; }
        public bool HaveRepeated { get; set; }
    }
}