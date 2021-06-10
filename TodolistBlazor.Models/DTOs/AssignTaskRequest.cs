using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodolistBlazor.Models.DTOs
{
    public class AssignTaskRequest
    {
        public Guid? UserID { get; set; }
        public Guid TaskID { get; set; }
    }
}
