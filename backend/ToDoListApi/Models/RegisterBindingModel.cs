using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace ToDoListApi.Models
{
    public class RegisterBindingModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}