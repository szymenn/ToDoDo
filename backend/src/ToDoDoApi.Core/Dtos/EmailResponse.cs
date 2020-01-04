using System.Collections.Generic;

namespace ToDoDoApi.Core.Dtos
{
    public class EmailResponse
    {
        public bool Successful => !(Errors?.Count > 0);

        public ICollection<string> Errors { get; set; }
    }
}