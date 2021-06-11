using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TodolistBlazor.Models.LoginModels;

namespace TodolistBlazorWasm.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/login", request);
            var content = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content);
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse;
            }
            else//nếu thành công thì ta add cái token vào local storage đồng thời cũng add vào header vào th httpclient, để dùng được local storage thì cài package blazor local
            {

            }
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
