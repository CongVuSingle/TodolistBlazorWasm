using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.API.Repositories;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;
using TodolistBlazor.Models.SeedWork;

namespace TodolistBlazor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        //api/task?name=
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskListSearch taskListSearch)
        {
            var pagedList = await _taskRepository.GetTaskList(taskListSearch);

            var taskDTOs = pagedList.Items.Select(x => new TaskDTO()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeID = x.AssigneeID,
                CreateDate = x.CreateDate,
                ID = x.ID,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A",
            }).ToList();
            return Ok(
                new PagedList<TaskDTO>(taskDTOs,
                pagedList.MetaData.TotalCount,
                pagedList.MetaData.CurrentPage,
                pagedList.MetaData.PageSize
                ));
        }


        //api/tasks/1
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetByID(id);
            if (task == null)
            {
                return NotFound($"{id} is not found");
            }
            return Ok(new TaskDTO()// dto đẻ hiển thị các field trên client
            {
                Name = task.Name,
                Status = task.Status,
                ID = task.ID,
                AssigneeID = task.AssigneeID,
                Priority = task.Priority,
                CreateDate = task.CreateDate,
            });
        }


        [HttpPost]
        public async Task<IActionResult> Creat([FromBody] TaskCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _taskRepository.Create(new Entities.Task()
            {
                ID = request.ID,
                Name = request.Name,
                Priority = request.Priority.HasValue ? request.Priority.Value : Models.Enums.Priority.Low,
                Status = Models.Enums.Status.Open,
                CreateDate = DateTime.Now,
            });
            return CreatedAtAction(nameof(GetByID), new { id = task.ID }, task);
            // nếu trả về là 200 code thì sẽ trả về hết field nào ko có thì null
            // còn field request để lúc điền vào ko bị lộ các field ko muốn show ra
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetByID(id);
            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.Name = request.Name;// update field nào đó mà ko update tất
            taskFromDb.Priority = request.Priority;

            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok(new TaskDTO()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                ID = taskResult.ID,
                AssigneeID = taskResult.AssigneeID,
                Priority = taskResult.Priority,
                CreateDate = taskResult.CreateDate,
            });
        }
        //request là những field cần thao tác từ người dùng
        //DTO là những gì mà db trả ra khi có 200 code, nhưng sẽ ko trả tất cả


        [HttpPut]
        [Route("{id}/assign")]
        public async Task<IActionResult> UpdateAssignTask([FromRoute] Guid id, [FromBody] AssignTaskRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetByID(id);
            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.AssigneeID = request.UserID.Value == Guid.Empty ? null : request.UserID.Value;// update field nào đó mà ko update tất

            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok(new TaskDTO()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                ID = taskResult.ID,
                AssigneeID = taskResult.AssigneeID,
                Priority = taskResult.Priority,
                CreateDate = taskResult.CreateDate,
            });
        }

        //api/tasks/1
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetByID(id);
            if (task == null)
            {
                return NotFound($"{id} is not found");
            }

            await _taskRepository.Delete(task);
            return Ok(new TaskDTO()// dto đẻ hiển thị các field trên client
            {
                Name = task.Name,
                Status = task.Status,
                ID = task.ID,
                AssigneeID = task.AssigneeID,
                Priority = task.Priority,
                CreateDate = task.CreateDate,
            });
        }
    }
}
