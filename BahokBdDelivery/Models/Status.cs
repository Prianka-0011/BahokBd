using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public partial class Status
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string PremStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
    }
}
