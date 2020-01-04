using System.ComponentModel.DataAnnotations;

namespace ToDoDoApi.Web.Models
{
    public class RegisterBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
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