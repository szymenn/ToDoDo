using System;
using Microsoft.AspNetCore.Identity;

namespace ToDoDoApi.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public Guid ToDoId { get; set; }
    }
}