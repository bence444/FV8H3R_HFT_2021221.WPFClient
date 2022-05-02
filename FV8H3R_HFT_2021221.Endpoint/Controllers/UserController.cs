using FV8H3R_HFT_2021221.Logic;
using FV8H3R_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserLogic userLog;

        public UserController(UserLogic userLogic)
        {
            this.userLog = userLogic;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userLog.ReadAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return userLog.ReadOne(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            userLog.Create(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            userLog.Update(id, value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userLog.Delete(id);
        }
    }
}
