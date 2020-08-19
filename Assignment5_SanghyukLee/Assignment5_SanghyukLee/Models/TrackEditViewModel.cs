using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5_SanghyukLee.Models
{
    public class TrackEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }

    }
}