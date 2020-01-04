using System;

namespace ToDoDoApi.Core.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
    }
}