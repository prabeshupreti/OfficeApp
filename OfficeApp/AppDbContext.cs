
using Microsoft.EntityFrameworkCore;
using OfficeApp.Models;

namespace OfficeApp
{

    public class AppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}