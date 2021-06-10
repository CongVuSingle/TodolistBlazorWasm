using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodolistBlazor.Models.Enums;

namespace TodolistBlazor.Models.DTOs
{
    public class TaskDTO
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public Guid? AssigneeID { get; set; } // assignee la nguoi dc giao task nay

        public string AssigneeName { get; set; }

        public DateTime CreateDate { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
    //Dto thì ko có các validation
    //taskcreateRequest thì có validation
}
