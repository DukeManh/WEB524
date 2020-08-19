using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment4.Models
{
    public class ArtistWithDetailViewModel : ArtistBaseViewModel
    {

        public ArtistWithDetailViewModel()
        {
            Albums = new List<Album>();
            AlbumNames = new List<string>();
            BirthOrStartDate = DateTime.Now;
        }

        public IEnumerable<Album> Albums { get; set; }

        public IEnumerable<string> AlbumNames { get; set; }
    }
}