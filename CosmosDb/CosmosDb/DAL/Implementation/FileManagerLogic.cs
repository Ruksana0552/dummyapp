using Azure.Storage.Blobs;
using CosmosDb.DAL.Abstraction;
using CosmosDb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.DAL.Implementation
{
    public class FileManagerLogic : IFileManagerLogic
    {
        private readonly BlobServiceClient _blobServiceClient;
        public FileManagerLogic(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task Upload(FileModel model)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("container552");

            var blobClient = blobContainer.GetBlobClient(model.ImageFile.FileName);
            
            await blobClient.UploadAsync(model.ImageFile.OpenReadStream());
                
                }


        public async Task<byte[]> Get(string imageName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("container552");

            var blobClient = blobContainer.GetBlobClient(imageName);

            var downloadContent = await blobClient.DownloadAsync();

            using (MemoryStream ms = new MemoryStream())
            {
                await downloadContent.Value.Content.CopyToAsync(ms);

                return ms.ToArray();
            }

         


        }

   // public async Task Delete(string imageName,string id)
             public async Task Delete( string Filename)
        {

            var blobContainer = _blobServiceClient.GetBlobContainerClient("container552");

            var blobClient = blobContainer.GetBlobClient(Filename);

            await blobClient.DeleteAsync();

        }
    }
}
