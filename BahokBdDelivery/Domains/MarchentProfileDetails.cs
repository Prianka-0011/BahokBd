﻿using System;
using System.Collections.Generic;

namespace BahokBdDelivery.Domains
{
    public partial class MarchentProfileDetails
    {
        public MarchentProfileDetails()
        {
            PickupLocations = new HashSet<PickupLocations>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
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

        public virtual ICollection<PickupLocations> PickupLocations { get; set; }
    }
}
