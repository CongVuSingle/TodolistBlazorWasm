using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;
using Task = TodolistBlazor.API.Entities.Task;

namespace TodolistBlazor.API.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetTaskList(TaskListSearch taskListsearch);

        Task<Task> Create(Task task);

        Task<Task> Update(Task task);

        Task<Task> Delete(Task task);

        Task<Task> GetByID(Guid id);
    }
}
