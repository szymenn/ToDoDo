using System;

namespace ToDoDoApi.Core.Entities
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}