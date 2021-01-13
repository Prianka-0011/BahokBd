using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{

    public partial class DeliveryAreaPrices
    {
        public DeliveryAreaPrices()
        {
            MarchentCharge = new HashSet<MarchentCharge>();
        }

        public Guid Id { get; set; }
        public string Area { get; set; }
        public decimal BaseChargeAmount { get; set; }
        public decimal IncreaseChargePerKg { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }

        public virtual ICollection<MarchentCharge> MarchentCharge { get; set; }
    }
}
