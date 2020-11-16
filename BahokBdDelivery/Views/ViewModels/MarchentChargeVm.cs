using BahokBdDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.ViewModels
{
    public class MarchentChargeVm
    {
        public Guid? Id { get; set; }
        public Guid? MarchentId { get; set; }
        public string Area { get; set; }
        public decimal BaseChargeAmount { get; set; }
        public decimal IncreaseChargePerKg { get; set; }
        public bool IsSelected { get; set; }

    }
}
