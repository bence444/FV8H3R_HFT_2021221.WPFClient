using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using FV8H3R_HFT_2021221.Endpoint.Services;
using FV8H3R_HFT_2021221.Logic;
using FV8H3R_HFT_2021221.Models;
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
        IHubContext<SignalRHub> hub;

        public UserController(UserLogic userLogic, IHubContext<SignalRHub> hub)
        {
            this.userLog = userLogic;
            this.hub = hub;
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
            hub.Clients.All.SendAsync("User added", value);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public void Put([FromBody] User value)
        {
            userLog.Update(value);
            hub.Clients.All.SendAsync("User updated", value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userLog.Delete(id);
            hub.Clients.All.SendAsync("User removed", id);
        }
    }
}
