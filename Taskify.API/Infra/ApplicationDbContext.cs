using Microsoft.EntityFrameworkCore;
using Taskify.API.Models;

namespace Taskify.API.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        {}

        public DbSet<Tasks> Tasks { get; set; }
    }
}
