using System;

namespace ToDoDoApi.Web.Models
{
    public class ToDoApiModel
    {
        public string Task { get; set; }
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
    }
}