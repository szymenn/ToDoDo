using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Models
{
    public class ToDoBindingModel
    {
        [Required]
        public string Task { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}