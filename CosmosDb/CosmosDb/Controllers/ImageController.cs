using CosmosDb.DAL.Abstraction;
using CosmosDb.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EnableCors")]
    public class ImageController : ControllerBase
    {
        private readonly IFileManagerLogic _fileManagerLogic;

        public ImageController(IFileManagerLogic fileManagerLogic)
        {
            _fileManagerLogic = fileManagerLogic;
        }

     
        [HttpPost("upload/{id}")]
        public async Task<IActionResult> Upload([FromForm]FileModel model)
        {
            if(model.ImageFile!=null)
            {
                await _fileManagerLogic.Upload(model);
            }
            return Ok();
        }

      
        [HttpGet("get")]
        public async Task<IActionResult> Get(string fileName)
        {
            var imgBytes = await _fileManagerLogic.Get(fileName);

            return File(imgBytes, "image/.png");
        }

      
        [HttpGet("download")]
        public async Task<IActionResult> Download(string fileName)
        {
            var imagBytes = await _fileManagerLogic.Get(fileName);

            return new FileContentResult(imagBytes, "application/octet-stream")
            {
                FileDownloadName = Guid.NewGuid().ToString() + ".png",
            };
        }
   [HttpDelete("delete/{Filename}")]
    public async Task<IActionResult> Delete(string Filename)
           
        {
            await _fileManagerLogic.Delete(Filename);
            return Ok("deleted");
        }
    }
}
