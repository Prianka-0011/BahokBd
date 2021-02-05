using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahokBdDelivery.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string Degignation { get; set; }
        public string Description { get; set; }
        public int UniqueCode { get; set; }
        public bool Status { get; set; }
    }
}
