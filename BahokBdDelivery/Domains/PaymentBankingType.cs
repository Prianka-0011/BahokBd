using System;
using System.Collections.Generic;

namespace BahokBdDelivery.Domains
{
    public partial class PaymentBankingType
    {
        public PaymentBankingType()
        {
            PaymentBankingOrganization = new HashSet<PaymentBankingOrganization>();
        }

        public Guid Id { get; set; }
        public string BankingMethodName { get; set; }

        public virtual ICollection<PaymentBankingOrganization> PaymentBankingOrganization { get; set; }
    }
}
