using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;

namespace TodolistBlazorWasm.Services
{
    public interface ITaskAPIClient
    {
        Task<List<TaskDTO>> GetTaskList(TaskListSearch taskListSearch);

        Task<TaskDTO> GetTaskDetail(string id);

        Task<bool> CreateTask(TaskCreateRequest request);

        Task<bool> UpdateTask(Guid ID, TaskUpdateRequest request);
        Task<bool> UpdateAssignTask(Guid ID, AssignTaskRequest request);

        Task<bool> DeleteTask(Guid ID);

    }
}
