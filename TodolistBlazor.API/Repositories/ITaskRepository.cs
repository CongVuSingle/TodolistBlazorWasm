using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;
using TodolistBlazor.Models.SeedWork;
using Task = TodolistBlazor.API.Entities.Task;

namespace TodolistBlazor.API.Repositories
{
    public interface ITaskRepository
    {
        Task<PagedList<Task>> GetTaskList(TaskListSearch taskListsearch); // b32-5:50 page list trả về 1 object

        Task<Task> Create(Task task);

        Task<Task> Update(Task task);

        Task<Task> Delete(Task task);

        Task<Task> GetByID(Guid id);
    }
}
