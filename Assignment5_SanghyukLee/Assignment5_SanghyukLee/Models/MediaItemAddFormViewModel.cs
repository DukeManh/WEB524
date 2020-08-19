using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5_SanghyukLee.Models
{
    public class MediaItemAddFormViewModel : MediaItemAddViewModel
    {

        [Required, Display(Name = "Media upload"), DataType(DataType.Upload)]
        public string MediaUpload { get; set; }
    }
}