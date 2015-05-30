using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arcus10.Models
{
    public class Guests
    {
        [Required]
        [DataType(DataType.Text)]
        public string guest_name { get; set; }

        public string guest_city { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string guest_cnic { get; set; }

        public string guest_address { get; set; }

        public string guest_phone { get; set; }

        public string guest_company { get; set; }

        public string guest_email { get; set; }

        public string guest_notes { get; set; }

    }
}