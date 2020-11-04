using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public class MarchentProfile
    {
        public Guid Id { get; set; }
        //Personal info
        public string Name { get; set; }
        public string  Email { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        //Business info
        public string BusinessName { get; set; }
        public string BusinessLink { get; set; }
        public string BusinessAddress { get; set; }
        //Account info
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingName { get; set; }
        public string BranchName { get; set; }
        public int ProfileStatus { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime DateTime { get; set; }
        public string PaymentBankingTypeName { get; set; }
        public string PaymentBankingOrganizationName { get; set; }
        //public PaymentBankingType PaymentBankingType { get; set; }

    }
}
