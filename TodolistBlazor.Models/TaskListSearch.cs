using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodolistBlazor.Models.Enums;
using TodolistBlazor.Models.SeedWork;

namespace TodolistBlazor.Models
{
    public class TaskListSearch : PagingParameters
    {
        public string Name { get; set; }
        public Guid? AssigneeID { get; set; }
        public Priority? Priority { get; set; }
    }
}
