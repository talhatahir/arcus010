﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arcus10.Models
{
    public class Rooms
    {

        public int room_id { get; set; }
        public string room_number { get; set; }
        public string room_name { get; set; }
        public string room_capacity { get; set; }
        public string room_type { get; set; }
        public string room_cost { get; set; }
        public string room_status { get; set; }

        public string room_availability{ get; set; }

    }
}