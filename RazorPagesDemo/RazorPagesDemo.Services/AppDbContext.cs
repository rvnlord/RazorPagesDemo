using Microsoft.EntityFrameworkCore;
using RazorPagesDemo.Models;

namespace RazorPagesDemo.Services
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> o) : base(o)
        {
            
        }
    }
}
