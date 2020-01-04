using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoDoApi.Web.Models
{
    public class ToDoBindingModel
    {
        [Required]
        public string Task { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}