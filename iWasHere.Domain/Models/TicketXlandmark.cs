﻿using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class TicketXlandmark
    {
        public int TicketXlandmarkId { get; set; }
        public int? LandmarkId { get; set; }
        public int? TicketTypeId { get; set; }
        public int? CurrencyTypeId { get; set; }
        public decimal? TicketValue { get; set; }

        public virtual DictionaryCurrencyType CurrencyType { get; set; }
        public virtual Landmark Landmark { get; set; }
        public virtual DictionaryTicketType TicketType { get; set; }

        public TicketXlandmark(int ticketXlandmarkId, int? landmarkId, int? ticketTypeId, int? currencyTypeId, decimal? ticketValue)
        {
            TicketXlandmarkId = ticketXlandmarkId;
            LandmarkId = landmarkId;
            TicketTypeId = ticketTypeId;
            CurrencyTypeId = currencyTypeId;
            TicketValue = ticketValue;
        }

        public TicketXlandmark()
        {
        }
    }
}
