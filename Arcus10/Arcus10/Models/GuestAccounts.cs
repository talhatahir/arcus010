using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arcus10.Models
{
    public class GuestAccounts
    {
        public int guestaccounts_index { get; set; }

        public float guestaccount_credit { get; set; }

        public float guestaccount_debit { get; set; }

        public DateTime guestaccount_date { get; set; }

        public int fk_guest_id { get; set; }

        public float guestaccount_balance { get; set; }

        public int? fk_meal_id { get; set; }

        public int? fk_room_id { get; set; }

    }
}