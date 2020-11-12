using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public partial class BankBranch
    {
        public BankBranch()
        {
            MarchentPaymentDetails = new HashSet<MarchentPaymentDetails>();
        }

        public Guid Id { get; set; }
        public string BranchName { get; set; }
        public string RoutingName { get; set; }
        public Guid? BankId { get; set; }
        public bool? Status { get; set; }

        public virtual PaymentBankingOrganization Bank { get; set; }
        public virtual ICollection<MarchentPaymentDetails> MarchentPaymentDetails { get; set; }
    }
}
