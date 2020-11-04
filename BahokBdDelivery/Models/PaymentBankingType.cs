using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public class PaymentBankingType
    {
        public Guid Id { get; set; }
        public string BankingMethodName { get; set; }
        List<PaymentBankingOrganization>PaymentBankingOrganizations { get; set; }
    }
}
