using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public partial class MarchentPaymentDetails
    {
        public Guid Id { get; set; }
        public Guid MarchentId { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public Guid? PaymentNameId { get; set; }
        public Guid? BranchId { get; set; }
        public string RoutingName { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }

        public virtual MarchentProfileDetail Marchent { get; set; }
    }
}

