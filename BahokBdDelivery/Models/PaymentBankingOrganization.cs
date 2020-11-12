using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public partial class PaymentBankingOrganization
    {
        public PaymentBankingOrganization()
        {
            BankBranch = new HashSet<BankBranch>();
        }

        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public Guid PaymentBankingTypeId { get; set; }
        public bool Status { get; set; }

        public virtual PaymentBankingType PaymentBankingType { get; set; }
        public virtual ICollection<BankBranch> BankBranch { get; set; }
    }
}
