using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class DictionaryCurrencyType
    {
        public DictionaryCurrencyType()
        {
            TicketXlandmark = new HashSet<TicketXlandmark>();
        }

        public int CurrencyTypeId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal CurrencyExRate { get; set; }

        public virtual ICollection<TicketXlandmark> TicketXlandmark { get; set; }
    }
}
