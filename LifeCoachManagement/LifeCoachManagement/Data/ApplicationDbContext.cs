using LifeCoachManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LifeCoachManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
