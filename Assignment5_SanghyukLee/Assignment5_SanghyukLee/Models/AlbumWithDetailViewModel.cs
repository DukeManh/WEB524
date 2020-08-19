using Assignment5_SanghyukLee.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5_SanghyukLee.Models
{
    public class AlbumWithDetailViewModel : AlbumBaseViewModel
    {

        public AlbumWithDetailViewModel()
        {
            Artists = new List<Artist>();
            Tracks = new List<Track>();
            ArtistNames = new List<string>();
            ReleaseDate = DateTime.Now;
        }

        public IEnumerable<string> ArtistNames { get; set; }

        public IEnumerable<Artist> Artists { get; set; }

        public IEnumerable<Track> Tracks { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album depiction")]
        public string Depiction { get; set; }
    }
}