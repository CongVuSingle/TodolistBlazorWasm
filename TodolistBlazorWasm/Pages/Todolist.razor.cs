using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodolistBlazor.Models;
using TodolistBlazor.Models.DTOs;
using TodolistBlazorWasm.Components;
using TodolistBlazorWasm.Pages.Components;
using TodolistBlazorWasm.Services;

namespace TodolistBlazorWasm.Pages
{
    public partial class Todolist // code behind
    {
        [Inject] private ITaskAPIClient TaskAPIClient { get; set; }

        protected Confirmation DeleteConfirmation { get; set; }

        protected AssignTask AssignTaskDialog { get; set; } //AssignTask này của component

        private Guid DeleteID { get; set; }

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

        public void OnDeleteTask(Guid deleteID)//button1
        {
            DeleteID = deleteID;// bấm nút 1 thì nút 2 sẽ bắt được ID
            DeleteConfirmation.Show();
        }

        public async Task OnConfirmDeleteTask(bool deleteConfirmed)//button2
        {
            if (deleteConfirmed)
            {
                await TaskAPIClient.DeleteTask(DeleteID);// sau khi ấn delete thì chưa xóa task đó mà phỉa reload tiếp
                Tasks = await TaskAPIClient.GetTaskList(TaskListSearch); //gọi lại dòng này sẽ reload lại
            }
        }
        public void OpenAssignPopup(Guid ID)
        {
            AssignTaskDialog.Show(ID);
        }

        public async Task AssignTaskSuccess(bool result)
        {
            if (result) // nếu đúng thì reload lại trang
            {
                Tasks = await TaskAPIClient.GetTaskList(TaskListSearch);
            }
        }
    }

    
}
