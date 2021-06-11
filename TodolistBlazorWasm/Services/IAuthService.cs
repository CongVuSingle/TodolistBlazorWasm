using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.Models.LoginModels;

namespace TodolistBlazorWasm.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request); // trả về login response, truyền vào login request

        Task LogOut();
    }
}
