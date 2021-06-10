using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.API.Repositories;
using TodolistBlazor.Models.DTOs;

namespace TodolistBlazor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUserList();

            var assignees = users.Select(x => new AssigneeDTO()
            {
                ID = x.Id,
                FullName = x.FirstName + ' ' + x.LastName
            });
            return Ok(assignees);
        }
    }
}
