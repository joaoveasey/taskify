using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.API.Infra;
using Taskify.API.Interfaces;
using Taskify.API.Repository;

namespace Taskify.API.Test
{
    public class TasksUnitTestController
    {
        public IUnitOfWork repository;
        public static DbContextOptions<ApplicationDbContext> dbContextOptions;

        public static string connectionString =
            "Server=127.0.0.1;Port=3306;User ID=root;Password=jvp_04022005;Database=taskify_db";

        static TasksUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        public TasksUnitTestController()
        {
            var context = new ApplicationDbContext(dbContextOptions);
            repository = new UnitOfWork(context);
        }
    }
}
