using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryCurrencyType
    {
        public DictionaryCurrencyType()
        {
            TicketXlandmark = new HashSet<TicketXlandmark>();
        }

        public int CurrencyTypeId { get; set; }
        [Required(ErrorMessage = "Error")]
        [DisplayName("Currency Code")]
        public string CurrencyCode { get; set; }
        [DisplayName("Currency Name")]
        public string CurrencyName { get; set; }
        [DisplayName("Currency Rate")]
        public decimal CurrencyExRate { get; set; }

        public virtual ICollection<TicketXlandmark> TicketXlandmark { get; set; }
    }
}
