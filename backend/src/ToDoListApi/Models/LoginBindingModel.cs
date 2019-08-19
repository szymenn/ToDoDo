using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Models
{
    public class LoginBindingModel
    {
        
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}