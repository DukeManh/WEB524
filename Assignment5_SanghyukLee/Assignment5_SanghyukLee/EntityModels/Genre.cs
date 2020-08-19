using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5_SanghyukLee.EntityModels
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}