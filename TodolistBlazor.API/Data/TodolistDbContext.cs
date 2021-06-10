using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TodolistBlazor.API.Entities;

namespace TodolistBlazor.API.Data
{
    public class TodolistDbContext : IdentityDbContext<User, Role, Guid>
    {
        public TodolistDbContext(DbContextOptions<TodolistDbContext> options) : base(options)
        {

        }

        public DbSet<Task> Tasks { get; set; }
    }
}
