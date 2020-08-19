using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment5_SanghyukLee.Models
{
    public class ArtistWithMediaInfoViewModel : ArtistWithDetailViewModel
    {

        public ArtistWithMediaInfoViewModel()
        {
            MediaItems = new List<MediaItemBaseViewModel>();
        }


        public IEnumerable<MediaItemBaseViewModel> MediaItems { get; set; }

    }
}