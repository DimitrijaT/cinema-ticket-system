﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketSystem.Web.Models.Domain
{
    public class TicketOrder
    {

        public Guid TicketId { get; set; }

        public Ticket OrderedTicket { get; set; }

        public Guid OrderId { get; set; }

        public Order UserOrder { get; set; }
    }
}
