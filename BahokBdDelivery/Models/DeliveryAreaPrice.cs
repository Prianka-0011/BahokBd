using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public class DeliveryAreaPrice
    {
        public Guid Id { get; set; }
        public string Area { get; set; }
        public double Price { get; set; }
    }
}
