using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.API.Data;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;
using Task = TodolistBlazor.API.Entities.Task;

namespace TodolistBlazor.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodolistDbContext _context;

        public TaskRepository(TodolistDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Task>> GetTaskList(TaskListSearch taskListSearch)
        {
            var query = _context.Tasks.Include(x=>x.Assignee).AsQueryable();
            if (!string.IsNullOrEmpty(taskListSearch.Name))
            {
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));
            }
            if(taskListSearch.AssigneeID.HasValue)
            {
                query = query.Where(x => x.AssigneeID.Value == taskListSearch.AssigneeID.Value);
            }
            if (taskListSearch.Priority.HasValue)
            {
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);
            }

            return await query.OrderByDescending(x=>x.CreateDate).ToListAsync();
        }


        //thay vì xử lý getlist bằng DTO ở đây thi nên xử lý bên controller
        //public async Task<IEnumerable<TaskDTO>> GetTaskList()
        //{
        //    return await _context.Tasks.Include(x => x.Assignee).Select(x => new TaskDTO()
        //    {
        //        Status = x.Status,
        //        Name = x.Name,
        //        AssigneeID = x.AssigneeID,
        //        CreateDate = x.CreateDate,
        //        ID = x.ID,
        //        AssigneeName = x.Assignee.FirstName + ' ' + x.Assignee.LastName,
        //    }).ToListAsync();
        //}

        public async Task<Task> Create(Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> Delete(Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> GetByID(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<Task> Update(Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
