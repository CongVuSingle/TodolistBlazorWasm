using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodolistBlazor.Models.DTOs;

namespace TodolistBlazorWasm.Services
{
    public class UserAPIClient : IUserAPIClient
    {
        public HttpClient _httpClient;

        public UserAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<AssigneeDTO>> GetAssignees()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AssigneeDTO>>("/api/users");
            return result;
        }
    }
}
