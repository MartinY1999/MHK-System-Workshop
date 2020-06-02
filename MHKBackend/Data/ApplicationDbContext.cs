using MHKBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MHKBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Finance> Finances { get; set; }
        public DbSet<Project> Projects { get; set; } 
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Support> Supports { get; set; }
    }
}