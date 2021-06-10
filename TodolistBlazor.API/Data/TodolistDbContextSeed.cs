using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.API.Entities;
using TodolistBlazor.Models.Enums;
using Task = System.Threading.Tasks.Task;

namespace TodolistBlazor.API.Data
{
    public class TodolistDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public async Task SeedAsync(TodolistDbContext context, ILogger<TodolistDbContextSeed> logger)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mr",
                    LastName = "Cong",
                    Email = "admin@gmail.com",
                    PhoneNumber = "123456789",
                    UserName = "admin",

                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                context.Users.Add(user);
                context.SaveChanges();
            }
            if (!context.Tasks.Any())
            {
                context.Tasks.Add(new Entities.Task()
                { 
                    ID = Guid.NewGuid(),
                    Name = "Same task 1",
                    CreateDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
