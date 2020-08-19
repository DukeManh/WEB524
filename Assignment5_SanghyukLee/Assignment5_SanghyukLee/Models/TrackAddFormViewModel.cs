using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5_SanghyukLee.Models
{
    public class TrackAddFormViewModel : TrackAddViewModel
    {
        [Required, Display(Name = "Track audio"), DataType(DataType.Upload)]
        public string AudioUpload { get; set; }
    }
}