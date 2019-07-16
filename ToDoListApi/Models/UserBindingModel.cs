using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Models
{
    public class UserBindingModel
    {
        [Required]
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}