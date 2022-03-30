using CosmosDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.DAL.Abstraction
{
  public  interface IFileManagerLogic
    {
       public Task Upload(FileModel model);
        Task<byte[]> Get(string imageName);

        public  Task Delete(string imageName);
        
    }
}
