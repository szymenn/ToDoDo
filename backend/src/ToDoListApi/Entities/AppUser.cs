using System;
using Microsoft.AspNetCore.Identity;

namespace ToDoListApi.Entities
{
    public class AppUser : IdentityUser
    {
        public Guid ToDoId { get; set; }
    }
}