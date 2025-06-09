
using Microsoft.EntityFrameworkCore;
using OfficeApp.Models;

namespace OfficeApp
{

    public class AppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}