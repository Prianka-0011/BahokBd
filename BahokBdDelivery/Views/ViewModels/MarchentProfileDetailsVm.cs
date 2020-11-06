using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.ViewModels
{
    public class MarchentProfileDetailsVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Logo { get; set; }
        public string DisplayImage { get; set; }
        public string DisplayLogo { get; set; }
        public string Phone { get; set; }
        public string BusinessName { get; set; }
        public string BusinessLink { get; set; }
        public string BusinessAddress { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingName { get; set; }
        public string BranchName { get; set; }
        public int? ProfileStatus { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime? DateTime { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public Guid? PaymentBankingId { get; set; }
    }
}
