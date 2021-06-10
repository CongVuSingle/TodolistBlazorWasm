using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;

namespace TodolistBlazorWasm.Services
{
    public class TaskAPIClient : ITaskAPIClient
    {
        public HttpClient _httpClient;

        public TaskAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateTask(TaskCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/tasks", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<TaskDTO> GetTaskDetail(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskDTO>($"api/tasks/{id}");
            return result;
        }

        public async Task<List<TaskDTO>> GetTaskList(TaskListSearch taskListSearch)
        {
            string url = $"/api/tasks?{taskListSearch.Name}&assigneeID={taskListSearch.AssigneeID}&priority={taskListSearch.Priority}";
            var result = await _httpClient.GetFromJsonAsync<List<TaskDTO>>(url);
            return result;
        }

        public async Task<bool> UpdateTask(Guid ID, TaskUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{ID}", request);
            return result.IsSuccessStatusCode;
        }
    }
}
