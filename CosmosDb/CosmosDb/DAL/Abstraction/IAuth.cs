using CosmosDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.DAL.Abstraction
{
  public  interface IAuth
    {
      public AuthUser Authenticate(string userName, string Password);

    }
}
