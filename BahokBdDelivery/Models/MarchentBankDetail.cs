using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public class MarchentBankDetail
    {
        public Guid Id { get; set; }
        public Guid PaymentTypeId { get; set; }
        public PaymentBankingType PaymentType { get; set; }
    }
}
