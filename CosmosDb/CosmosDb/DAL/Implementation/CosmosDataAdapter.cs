using Azure.Storage.Blobs;
using CosmosDb.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CosmosDb.DAL
{
    public class CosmosDataAdapter : ICosmosDataAdapter
    {
        private readonly DocumentClient _client;
        private readonly string _accountUrl;
            private readonly string _primaryKey;
        private readonly BlobServiceClient _blobServiceClient;
        public CosmosDataAdapter(
        ICosmosConnection connection,
        IConfiguration config, BlobServiceClient blobServiceClient)
        {

            _accountUrl = config.GetValue<string>("Cosmos:AccountURL");
            _primaryKey = config.GetValue<string>("Cosmos:AuthKey");
            _client = new DocumentClient(new Uri(_accountUrl), _primaryKey);
            _blobServiceClient = blobServiceClient;
            
        }

        public  Task<UserInfo> UpsertUserAsync(UserInfo User)
        {
            ResourceResponse<Document> response = null;
            try
            {
                response = null;//await _client.UpsertDocumentAsync(_collectionUri, user);
            }
            catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
           
            return (dynamic)response.Resource;

        }
        
        public async Task<bool> CreateCollection(string dbName, string name)
        {
            await _client.CreateDocumentCollectionIfNotExistsAsync
            
                (UriFactory.CreateDatabaseUri(dbName), new DocumentCollection { Id = name });
           
            return true;
        }
        //crea
        public async Task<bool> CreateDatabase(string name)
        {
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = name });
            
            return true;
        }

        public async Task<bool> CreateDocument(string dbName, string name, UserInfo userInfo)
        {
           // userInfo.id = "d9e51c1e-1474-41d1-8f32-96deedd8f36a";

        
            await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), userInfo);

            var blobContainer = _blobServiceClient.GetBlobContainerClient("container552");


            var blobClient = blobContainer.GetBlobClient(userInfo.ImageFile.FileName);

            await blobClient.UploadAsync(userInfo.ImageFile.OpenReadStream());
            return true;
        }

        public async Task<UserInfo> DeleteUserAsync(string dbName, string name, string id,string Filename)
        {
            var collectionUri = UriFactory.CreateDocumentUri(dbName, name, id);

            var result = await _client.DeleteDocumentAsync(collectionUri);
           
            var blobContainer = _blobServiceClient.GetBlobContainerClient("container552");

            var blobClient = blobContainer.GetBlobClient(Filename);

            await blobClient.DeleteAsync();

            return (dynamic)result.Resource;
        }

        public async Task<dynamic> GetData(string dbName, string name)
        {
            var result = await _client.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(dbName, name),
                    
                new FeedOptions { MaxItemCount = 10 });
            

            return result;

         }
        public async Task<byte[]> GetFile(string imageName)
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
        public async Task<bool> PlaceOrder(string dbName, string name, Order order)
        {
            await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), order);

            return true;
        }

        public async Task<bool> CreateregCollection(string dbName, string name)
        {
            await _client.CreateDocumentCollectionIfNotExistsAsync

                  (UriFactory.CreateDatabaseUri(dbName), new DocumentCollection { Id = name });

            return true;
        }

        public async Task<bool> CreateregDocument(string dbName, string name, registration reg)
        {
            await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), reg);

            
            return true;

        }

        public async Task<dynamic> GetregData(string dbName, string name)
        {
            var result = await _client.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(dbName, name),

                 new FeedOptions { MaxItemCount = 10 });


            return result;
        }
    }
}
 