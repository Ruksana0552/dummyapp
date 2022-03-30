using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.Models
{
    public class registration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }
        public string mobile { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}
