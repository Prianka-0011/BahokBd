using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public partial class MarchentCharge
    {
        public Guid Id { get; set; }
        public Guid? MarchentId { get; set; }
        public string Location { get; set; }
        public decimal? BaseCharge { get; set; }
        public decimal? IncreaseCharge { get; set; }
        public Guid? DeliveryAreaPriceId { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }

        public virtual DeliveryAreaPrices DeliveryAreaPrice { get; set; }
        public virtual MarchentProfileDetail Marchent { get; set; }
    }
}
