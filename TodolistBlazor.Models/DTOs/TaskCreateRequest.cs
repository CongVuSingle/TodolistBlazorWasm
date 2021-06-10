using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodolistBlazor.Models.Enums;

namespace TodolistBlazor.Models.DTOs
{
    //taskCreateRequest là cần hiển thị những field nào ra ở trên API
    //DTO là cần lưu gì và DB
    public class TaskCreateRequest
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        [MaxLength(250, ErrorMessage = "You cannot fill task name over than 20 characters")]
        [Required(ErrorMessage = "Please enter your task name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select your task priority")]
        public Priority? Priority { get; set; }

    }
}
