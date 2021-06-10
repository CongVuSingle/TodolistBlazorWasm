using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TodolistBlazor.Models.Enums;

namespace TodolistBlazor.API.Entities
{
    public class Task
    {
        [Key]
        public Guid ID { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public Guid? AssigneeID { get; set; } // assignee la nguoi dc giao task nay

        [ForeignKey("AssigneeID")]
        public User Assignee { get; set; }

        public DateTime CreateDate { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

    }
}
