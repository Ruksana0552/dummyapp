using CosmosDb.DAL;
using CosmosDb.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.Controllers
{

    //[Authorize]

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EnableCors")]
    public class Cosmos : ControllerBase
    {
        ICosmosDataAdapter _adapter;
        public Cosmos(ICosmosDataAdapter adapter)
        {
            _adapter = adapter;
        }

        [HttpGet("createdb")]
        public async Task<IActionResult> CreateDatabase()
        {
            var result = await _adapter.CreateDatabase("test-db");

            return Ok(result);
        }

        [HttpGet("createcollection")]
        public async Task<IActionResult> CreateCollection()
        {
            var result = await _adapter.CreateCollection("test-db", "test-collection");

            return Ok(result);
        }
       
        
        [HttpGet("createregcollection")]
        public async Task<IActionResult> CreateregCollection()
        {
            var result = await _adapter.CreateCollection("test-db", "reg-collection");
            return Ok(result);
        }


        [HttpPost("createregdocument")]
        public async Task<IActionResult> CreateregDocument([FromForm] registration reg)
        {
            var result1 = await _adapter.CreateregDocument("test-db", "reg-collection",reg);
           
            return Ok(result1);
        }


        [HttpPost("createdocument")]
        public async Task<IActionResult> CreateDocument([FromForm] UserInfo user)
        {
            var result = await _adapter.CreateDocument("test-db", "test-collection", user);
            return Ok(result);
        }

        [HttpPost("placeorder")]
        public async Task<IActionResult> Post([FromForm] Order order)
        {
            var result = await _adapter.PlaceOrder("test-db", "test-collection", order);
            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _adapter.GetData("test-db", "test-collection");
            return Ok(result);
        }


        //[HttpPost("save")]
        //public async Task<IActionResult> Post([FromBody] UserInfo user)
        //{
        //    var result = await _adapter.UpsertUserAsync(user);
        //    return Ok();
        //}

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id,string Filename)
        {
            var result = await _adapter.DeleteUserAsync("test-db", "test-collection", id,Filename);
            return Ok(true);
        }

        [HttpGet("getfilename")]
        public async Task<IActionResult> Get(string fileName)
        {
            var imgBytes = await _adapter.GetFile(fileName);

            return File(imgBytes, "image/.png");
        }
        
        [HttpGet("download")]
        public async Task<IActionResult> Download(string fileName)
        {
            var imagBytes = await _adapter.GetFile(fileName);

            return new FileContentResult(imagBytes, "application/octet-stream")
            {
                FileDownloadName = Guid.NewGuid().ToString() + ".png",
            };
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
