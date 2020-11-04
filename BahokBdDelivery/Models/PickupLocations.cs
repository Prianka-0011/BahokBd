using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public partial class PickupLocations
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; }
        public string DetailAddress { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhone { get; set; }
        public Guid MarchentId { get; set; }
        public Guid AreaPriceId { get; set; }

       // public virtual DeliveryAreaPrices AreaPrice { get; set; }
        public virtual MarchentProfileDetails Marchent { get; set; }
    }
}
