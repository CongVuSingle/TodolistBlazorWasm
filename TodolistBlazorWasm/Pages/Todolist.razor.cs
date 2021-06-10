using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;
using TodolistBlazorWasm.Services;

namespace TodolistBlazorWasm.Pages
{
    public partial class Todolist // code behind
    {
        [Inject] private ITaskAPIClient TaskAPIClient { get; set; }
        
        private List<TaskDTO> Tasks;

        private TaskListSearch TaskListSearch = new TaskListSearch();
        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskAPIClient.GetTaskList(TaskListSearch);
        }
        
        public async Task SearchTask(TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            Tasks = await TaskAPIClient.GetTaskList(TaskListSearch);
        }
    }

    
}
