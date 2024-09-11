using Taskify.API.Infra;
using Taskify.API.Interfaces;

namespace Taskify.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITasksRepository _tasksRepository;
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ITasksRepository TasksRepository
        {
            get
            {
                if (_tasksRepository is null)
                    _tasksRepository = new TasksRepository(_context);

                return _tasksRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
