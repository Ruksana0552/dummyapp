using CosmosDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.DAL.Abstraction
{
   public interface Iregister
    {
        //Task<bool> CreateDatabase(string name);
        Task<bool> CreateregCollection(string dbName, string name);
        Task<bool> CreateregDocument(string dbName, string name, registration reg);

        Task<registration> GetregData(string dbName, string name);
    }
}
