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
         [EnableCors("EnableCors")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private IAuth aservice;
        public Authentication(IAuth _aservice)
        {
            aservice = _aservice;
        }
        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] AuthUser model)
        {
            var user = aservice.Authenticate(model.UserName, model.Password);
            if (user == null)
                return BadRequest(new { message = "incorrect credentials" });

            return Ok(user);

        }
    }
}
