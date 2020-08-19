using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment4.Models
{
    public class TrackAddViewModel
    {
        public TrackAddViewModel()
        {
            Albums = new List<Album>();
        }

        public string Clerk { get; set; } // holds the username (e.g. amanda@example.com) of the authenticated user who is in the process of adding a new Track object

        [StringLength(50), Display(Name = "Composer name (comma-separated)"), Required]
        public string Composers { get; set; } //holds the names of the track's composers. user will simply type comma separators between the names of multiple composers so no hard-coding is required.

        public string Genre { get; set; } //holds a genre string/value

        [Required, StringLength(40), Display(Name = "Track name")]
        public string Name { get; set; }

        [Display(Name = "Track genre")]
        public SelectList TrackGenreList { get; set; }

        public int AlbumId { get; set; }
        public string AlbumName { get; set; }

        public IEnumerable<Album> Albums { get; set; }
    }
}