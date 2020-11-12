using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public partial class MarchentProfileDetail
    {
        public MarchentProfileDetail()
        {
            MarchentCharge = new HashSet<MarchentCharge>();
            MarchentPaymentDetails = new HashSet<MarchentPaymentDetails>();
            MarchentStore = new HashSet<MarchentStore>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string BusinessName { get; set; }
        public string BusinessLink { get; set; }
        public string BusinessAddress { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingName { get; set; }
        public string BranchName { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public Guid? PaymentBankingId { get; set; }
        public virtual ICollection<MarchentCharge> MarchentCharge { get; set; }
        public virtual ICollection<MarchentPaymentDetails> MarchentPaymentDetails { get; set; }
        public virtual ICollection<MarchentStore> MarchentStore { get; set; }
    }
}
