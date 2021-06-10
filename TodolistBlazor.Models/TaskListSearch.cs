using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodolistBlazor.Models.Enums;

namespace TodolistBlazor.Models
{
    public class TaskListSearch
    {
        public string Name { get; set; }
        public Guid? AssigneeID { get; set; }
        public Priority? Priority { get; set; }
    }
}
