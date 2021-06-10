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

        

        private TaskListSearch TaskLiskSearch = new TaskListSearch();
        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskAPIClient.GetTaskList(TaskLiskSearch);

            
        }
        
    }

    
}
