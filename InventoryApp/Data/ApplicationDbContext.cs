using System;
using System.Collections.Generic;
using System.Text;
using InventoryApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<InventoryUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<InventoryApp.Models.Item> Item { get; set; }
        public DbSet<InventoryApp.Models.InventoryItem> InventoryItem { get; set; }
        public DbSet<InventoryApp.Models.Checkout> Checkout { get; set; }
    }
}
