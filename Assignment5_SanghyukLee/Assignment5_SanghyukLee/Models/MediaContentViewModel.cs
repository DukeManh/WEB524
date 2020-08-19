using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment5_SanghyukLee.Models
{
    public class MediaContentViewModel
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

    }
}