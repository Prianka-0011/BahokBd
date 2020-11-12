using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public partial class PaymentBankingType
    {
        public PaymentBankingType()
        {
            PaymentBankingOrganization = new HashSet<PaymentBankingOrganization>();
        }

        public Guid Id { get; set; }
        public string BankingMethodName { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<PaymentBankingOrganization> PaymentBankingOrganization { get; set; }
    }
}
