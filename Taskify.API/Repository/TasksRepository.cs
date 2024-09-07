﻿using Microsoft.EntityFrameworkCore;
using Taskify.API.Infra;
using Taskify.API.Interfaces;
using Taskify.API.Models;

namespace Taskify.API.Repository
{
    public class TasksRepository : ITasksRepository
    {

        protected readonly ApplicationDbContext _context;

        public TasksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tasks>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();

        }

        public async Task<Tasks> GetByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            return task;
        }

        public async Task<Tasks> AddAsync(Tasks task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Tasks> UpdateAsync(Tasks task)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Tasks> RemoveAsync(Tasks task)
        {
            _context.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }
    }
}
