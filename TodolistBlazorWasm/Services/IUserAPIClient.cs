using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.Models.DTOs;

namespace TodolistBlazorWasm.Services
{
    public interface IUserAPIClient
    {
        Task<List<AssigneeDTO>> GetAssignees();
    }
}
