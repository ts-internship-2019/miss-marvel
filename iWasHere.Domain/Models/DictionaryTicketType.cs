using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryTicketType
    {
        public DictionaryTicketType()
        {
            TicketXlandmark = new HashSet<TicketXlandmark>();
        }

        [Key]
        public int TicketTypeId { get; set; }

        [Required]
        [StringLength(256)]
        public string TicketTypeName { get; set; }

        public virtual ICollection<TicketXlandmark> TicketXlandmark { get; set; }
    }
}
