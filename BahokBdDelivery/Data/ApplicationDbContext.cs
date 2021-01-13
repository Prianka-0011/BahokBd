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
        //public virtual DbSet<BankBranches> BankBranches { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<DeliveryAreaPrices> DeliveryAreaPrices { get; set; }
        public virtual DbSet<MarchentCharge> MarchentCharge { get; set; }
        public virtual DbSet<MarchentPaymentDetails> MarchentPaymentDetails { get; set; }
        public virtual DbSet<MarchentProfileDetail> MarchentProfileDetail { get; set; }
        public virtual DbSet<MarchentStore> MarchentStore { get; set; }
        public virtual DbSet<PaymentBankingOrganization> PaymentBankingOrganization { get; set; }
        public virtual DbSet<PaymentBankingType> PaymentBankingType { get; set; }
        public virtual DbSet<Status> Status { get; set; }


    }
}
