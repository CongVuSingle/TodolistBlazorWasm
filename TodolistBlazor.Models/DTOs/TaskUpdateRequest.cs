using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodolistBlazor.Models.Enums;

namespace TodolistBlazor.Models.DTOs
{
    public class TaskUpdateRequest // khác với taskCreateRequest là ko cần ID nhưng cx cần tách ra cho rõ
    {

        public string Name { get; set; }

        public Priority Priority { get; set; }

    }
}
