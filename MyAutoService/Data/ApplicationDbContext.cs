using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Models;

namespace MyAutoService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ServicType> ServicType { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<ServiceDetails> ServicDetalis{ get; set; }
        public DbSet<ServiceHeader> ServiceHeaders { get; set; }

        public DbSet<ServicesShoppingCart> ServicesShoppingCarts { get; set; }
    }
}