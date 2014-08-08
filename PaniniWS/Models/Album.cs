using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaniniWS.Models
{
    public class Album
    {
        public int AlbumID { get; set; }
        public string Name { get; set; }
        public string AlbumCoverFilePath { get; set; }

        public virtual ICollection<AlbumPage> AlbumPages { get; set; }
    }
}