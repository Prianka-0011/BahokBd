using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Views.ViewModels
{
    public class MarchentStoreVm
    {
        public Guid Id { get; set; }
        public Guid? MarchentId { get; set; }
        public Guid? LocationId { get; set; }
        public string StoreName { get; set; }
        public string ManagerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
    }
}
