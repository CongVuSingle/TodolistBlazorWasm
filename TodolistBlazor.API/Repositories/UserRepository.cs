using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodolistBlazor.API.Data;
using TodolistBlazor.API.Entities;
using TodolistBlazor.Models.DTOs;

namespace TodolistBlazor.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodolistDbContext _context;

        public UserRepository(TodolistDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUserList() => await _context.Users.ToListAsync();

    }
}
