using BahokBdDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.ViewModels
{
    public class MarchentApproveVm
    {
        public Guid? MarchentId { get; set; }
        public Guid[] AreaId { get; set; }

    }
}
