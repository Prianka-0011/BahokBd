using System;
using System.Collections.Generic;
using System.Text;
using BahokBdDelivery.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BahokBdDelivery.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       // public DbSet<DeliveryAreaPrices> DeliveryAreaPrices { get; set; }
        public DbSet<PaymentBankingType> PaymentBankingType { get; set; }
        public DbSet<PaymentBankingOrganization> PaymentBankingOrganization { get; set; }
       
        public DbSet<PickupLocations> PickupLocations { get; set; }
        public DbSet<DeliveryAreaPrices> DeliveryAreaPrices { get; set; }
        public DbSet<MarchentProfileDetails> MarchentProfileDetails { get; set; }
    }
}
