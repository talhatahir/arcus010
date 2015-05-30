using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arcus10.Models
{
    public class Staff
    {

        public int staff_id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string staff_fullname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string staff_address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string staff_email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string staff_phone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string staff_nic { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string staff_position { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string staff_account { get; set; }


        public string staff_salary { get; set; }

    }
}