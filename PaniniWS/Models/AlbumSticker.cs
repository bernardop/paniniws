using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaniniWS.Models
{
    public class AlbumSticker
    {
        public int AlbumStickerID { get; set; }
        public virtual AlbumPage AlbumPage { get; set; }
        public int StickerNumber { get; set; }
        public string Description { get; set; }
        public string StickerImageFilePath { get; set; }
    }
}
