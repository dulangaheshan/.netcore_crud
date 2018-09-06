using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Testing2.DataProvider;
using Testing2.Model;

namespace Testing2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataProvider.DataProvider _dataprovider;
        public ValuesController()
        {
            _dataprovider = new DataProvider.DataProvider();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return this._dataprovider.GetUser(id);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            if (this._dataprovider.AddUser(user))
            {
                return Ok("login sucess");
            }
            else
            {
                return BadRequest("user already exists");
            }

        }
        // POST api/login
        
        [HttpPost("login")]
        public ActionResult Post([FromBody] Login login)
        {
            if (this._dataprovider.UserLogin(login))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Username or password is incorrect");
            }
            


        }
        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
