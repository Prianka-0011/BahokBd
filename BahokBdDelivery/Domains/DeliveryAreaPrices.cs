using System;
using System.Collections.Generic;

namespace BahokBdDelivery.Domains
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
