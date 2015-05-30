using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arcus10.Models
{
    public class Users
    {


            public int user_id { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            public string username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string password { get; set; }

            public string confirmpassword { get; set; }

            public string role { get; set; }

            public string fullname { get; set; }

     

    }
}