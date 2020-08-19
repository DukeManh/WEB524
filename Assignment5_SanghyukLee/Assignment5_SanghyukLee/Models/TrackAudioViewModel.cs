using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment5_SanghyukLee.Models
{
    public class TrackAudioViewModel
    {

        public int Id { get; set; }

        public byte[] Audio { get; set; }

        public string AudioContentType { get; set; }
    }
}