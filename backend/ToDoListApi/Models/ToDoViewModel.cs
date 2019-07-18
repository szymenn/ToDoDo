using System;

namespace ToDoListApi.Models
{
    public class ToDoViewModel
    {
        public string Task { get; set; }
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
    }
}