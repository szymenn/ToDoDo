using System;

namespace ToDoListApi.Models
{
    public class ToDoDto
    {
        public string Task { get; set; }
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
    }
}