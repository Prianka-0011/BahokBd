using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.ViewModels
{
    public class MarchentProfileDetailVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Logo { get; set; }
        public string DisplayImage { get; set; }
        public string DisplayLogo { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string BusinessName { get; set; }
        public string BusinessLink { get; set; }
        public string BusinessAddress { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingName { get; set; }
        public Guid? BranchId { get; set; }
        public int? ProfileStatus { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime DateTime { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public Guid? PaymentBankingId { get; set; }
        //public Guid? ExistPaymentTypeId { get; set; }
        //public Guid? ExistPaymentBankingId { get; set; }
        //odl bank detail
        public string OdlPaymentTypeName { get; set; }
        public string OdlBankName { get; set; }
        public string OdlBranchName { get; set; }
        public string OdlRouting { get; set; }
    }
}
