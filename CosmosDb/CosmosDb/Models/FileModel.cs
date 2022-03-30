using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.Models
{
    public class FileModel
    {
        public string id { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
