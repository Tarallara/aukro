using Aukro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aukro.Services
{
    class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=aukro.db");
        }

        public void SeedData()
        {
            if (!Categories.Any())
            {
                Categories.Add(new Category { Name = "Electro" });
                Categories.Add(new Category { Name = "House" });

                SaveChanges();
            }
        }
    }
}
