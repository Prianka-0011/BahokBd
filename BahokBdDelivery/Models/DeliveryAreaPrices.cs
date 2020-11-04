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
            PickupLocations = new HashSet<PickupLocations>();
        }

        public Guid Id { get; set; }
        public string Area { get; set; }
        public decimal BaseChargeAmount { get; set; }
        public decimal IncreaseChargePerKg { get; set; }

        public virtual ICollection<PickupLocations> PickupLocations { get; set; }
    }
}
