using HocMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace HocMVC.Data
{
    public class ApplicationDbContext : DbContext // dbcontext trong goi nuget core
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        //tao bang category
        public DbSet<Category> Categories { get; set; }
        //Insert
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DesplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DesplayOrder = 2 },
                new Category { Id = 3, Name = "Hisory", DesplayOrder = 3 },
                new Category { Id = 4, Name = "VietNam", DesplayOrder = 4 }
                );
        }
    }
}
