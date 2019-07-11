using System;
using Microsoft.AspNetCore.Identity;

namespace ToDoListApi.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ToDoId { get; set; }
    }
}