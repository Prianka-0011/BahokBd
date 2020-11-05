using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public class MarchantCharge
    {
        public Guid Id { get; set; }

        public Guid MarchentId { get; set; }
        //public MarchentProfile Marchent { get; set; }
        public Guid AreaPriceId { get; set; }
       // public DeliveryAreaPrices AreaPrice { get; set; }
    }
}
