using Microsoft.EntityFrameworkCore;
using Taskify.API.Models;

namespace Taskify.API.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {}

        public DbSet<Task> Tasks { get; set; }
    }
}
