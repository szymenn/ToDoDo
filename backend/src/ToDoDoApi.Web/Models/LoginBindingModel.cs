using System.ComponentModel.DataAnnotations;

namespace ToDoDoApi.Web.Models
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