using System.Collections.Generic;

namespace ToDoListApi.Email
{
    public class EmailResponse
    {
        public bool Successful => !(Errors?.Count > 0);

        public ICollection<string> Errors { get; set; }
    }
}