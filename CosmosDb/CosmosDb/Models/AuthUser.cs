using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.Models
{
    public class AuthUser
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
