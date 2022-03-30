using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.DAL
{
   public interface ICosmosConnection
    {
        Task<DocumentClient> InitializeAsync(string collectionId);
    }
}
