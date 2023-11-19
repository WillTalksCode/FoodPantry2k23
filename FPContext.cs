using FoodPantry2k23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FoodPantry2k23
{
    public class FPContext : DbContext
    {
        private object? services;
        private readonly IConfiguration _config;
        public DbSet<Person> People { get; set; }
        public DbSet<Household> Households { get; set; }
        public DbSet<Referral> Referrals { get; set; }


        public FPContext(IConfiguration config)
        {
            _config = config;
        }

       protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
           options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }

    }
}
