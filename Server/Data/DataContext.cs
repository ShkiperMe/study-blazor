using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using Fridge.Shared;

namespace Fridge.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes("admin"));
            }
            User adminUser = new User {
                Id = 1,
                Email = "admin@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DateCreated = DateTime.Now,
                Role = "Admin"
            };

            modelBuilder.Entity<User>().HasData( new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Refrigerator> Refrigerators { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
    }
}
