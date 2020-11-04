using System;
using System.Collections.Generic;

namespace BahokBdDelivery.Domains
{
    public partial class PaymentBankingOrganization
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public Guid PaymentBankingTypeId { get; set; }

        public virtual PaymentBankingType PaymentBankingType { get; set; }
    }
}
