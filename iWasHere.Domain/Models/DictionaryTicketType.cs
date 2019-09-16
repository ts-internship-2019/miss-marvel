using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryTicketType
    {
        public DictionaryTicketType()
        {
            TicketXlandmark = new HashSet<TicketXlandmark>();
        }

        public int TicketTypeId { get; set; }
        public string TicketTypeName { get; set; }

        public virtual ICollection<TicketXlandmark> TicketXlandmark { get; set; }
    }
}
