using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public class PaymentBankingOrganization
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public Guid PaymentBankingTypeId { get; set; }
        public PaymentBankingType PaymentBankingType { get; set; }
    }
}
