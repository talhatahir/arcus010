using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arcus10.Models
{
    public class Bookings
    {

        public int fk_guest_id { get; set; }

        public int fk_room_id { get; set; }

        public DateTime booking_checkin { get; set; }

        public DateTime booking_checkout { get; set; }

        public bool booking_bActive { get; set; }

    }
}