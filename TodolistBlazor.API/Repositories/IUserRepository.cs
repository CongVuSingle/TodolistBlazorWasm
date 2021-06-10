using System.Collections.Generic;
using System.Threading.Tasks;
using TodolistBlazor.API.Entities;

namespace TodolistBlazor.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
