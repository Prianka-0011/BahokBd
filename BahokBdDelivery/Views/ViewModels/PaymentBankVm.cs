using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Views.ViewModels
{
    public class PaymentBankVm
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public Guid PaymentBankingTypeId { get; set; }
        public bool Status { get; set; }
        //public List<> Status { get; set; }to do
    }
}
