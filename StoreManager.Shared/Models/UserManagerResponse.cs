using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Shared.Models
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public bool Failed { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Email { get; set; }
        public string ExpiresOn { get; set; }
        public string Id { get; set; }
        public bool IsVerified { get; set; }
        public string IssuedOn { get; set; }
        public string JwToken { get; set; }
        public string UserName { get; set; }
    }
}
