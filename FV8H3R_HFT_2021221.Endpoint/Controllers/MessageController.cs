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
    public class MessageController : ControllerBase
    {
        MessageLogic msgLog;

        public MessageController(MessageLogic messageLogic)
        {
            this.msgLog = messageLogic;
        }

        // GET: api/<MessageController>
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return msgLog.ReadAll();
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        public Message Get(int id)
        {
            return msgLog.ReadOne(id);
        }

        // POST api/<MessageController>
        [HttpPost]
        public void Post([FromBody] Message value)
        {
            msgLog.Create(value);
        }

        // PUT api/<MessageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Message value)
        {
            msgLog.Update(value);
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            msgLog.Delete(id);
        }
    }
}
