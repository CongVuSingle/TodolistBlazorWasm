using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;
using TodolistBlazor.Models.SeedWork;

namespace TodolistBlazorWasm.Services
{
    public class TaskAPIClient : ITaskAPIClient
    {
        public HttpClient _httpClient;// httpCLient dùng để nhận data và gửi data tư web api thay cho webclient cũ

        public TaskAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateTask(TaskCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/tasks", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(Guid ID)
        {
            var result = await _httpClient.DeleteAsync($"/api/tasks/{ID}");
            return result.IsSuccessStatusCode;
        }

        public async Task<TaskDTO> GetTaskDetail(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskDTO>($"api/tasks/{id}");
            return result;
        }

        //public async Task<PagedList<TaskDTO>> GetTaskList(TaskListSearch taskListSearch)
        //{
        //    string url = $"/api/tasks?{taskListSearch.Name}" +
        //                 $"&assigneeID={taskListSearch.AssigneeID}" +
        //                 $"&priority={taskListSearch.Priority}";
        //    var result = await _httpClient.GetFromJsonAsync<List<TaskDTO>>(url);
        //    return result;
        //}

        public async Task<PagedList<TaskDTO>> GetTaskList(TaskListSearch taskListSearch)
        {
            //string url = $"/api/tasks?{taskListSearch.Name}" + 
            //             $"&assigneeID={taskListSearch.AssigneeID}" +  
            //             $"&priority={taskListSearch.Priority}";

            // tại đây truyền thêm pageNumber dùng queryBuilder cuar Utilities thay vì cộng chuỗi như trên
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(taskListSearch.Name))
            {
                queryStringParam.Add("name", taskListSearch.Name);
            }
            if (taskListSearch.AssigneeID.HasValue)
            {
                queryStringParam.Add("assigneeID", taskListSearch.AssigneeID.ToString());
            }
            if (taskListSearch.Priority.HasValue)
            {
                queryStringParam.Add("priority", taskListSearch.AssigneeID.ToString());
            }

            string url = QueryHelpers.AddQueryString("/api/tasks", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<TaskDTO>>(url);
            return result;
        }

        public async Task<bool> UpdateAssignTask(Guid ID, AssignTaskRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{ID}/assign", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTask(Guid ID, TaskUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{ID}", request);
            return result.IsSuccessStatusCode;
        }
    }
}
