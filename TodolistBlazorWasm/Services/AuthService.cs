using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TodolistBlazor.Models.LoginModels;

namespace TodolistBlazorWasm.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/login", loginRequest);
            var content = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse;
            }
            else//nếu thành công thì ta add cái token vào local storage đồng thời cũng add vào header vào th httpclient, để dùng được local storage thì cài package blazor local
            {
                await _localStorage.SetItemAsync("authToken", loginResponse.Token);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginRequest.UserName);//ép sang kiểu ApiAuthenticationStateProvider để gán cho nó là mark đã đăng nhập(hay là quản lý trang thái đã authen)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token); ;// gán cái token này vào cấc request từ httpclient
                return loginResponse;
            }
        }

        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
