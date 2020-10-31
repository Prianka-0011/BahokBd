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
        public DbSet<DeliveryAreaPrice> DeliveryAreaPrices { get; set; }
    }
}
