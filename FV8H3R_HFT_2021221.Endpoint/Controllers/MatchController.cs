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
    public class MatchController : ControllerBase
    {
        MatchLogic matchLog;

        public MatchController(MatchLogic matchLogic)
        {
            this.matchLog = matchLogic;
        }

        // GET: api/<MatchController>
        [HttpGet]
        public IEnumerable<Match> Get()
        {
            return matchLog.ReadAll();
        }

        // GET api/<MatchController>/5
        [HttpGet("{id}")]
        public Match Get(int id)
        {
            return matchLog.ReadOne(id);
        }

        // POST api/<MatchController>
        [HttpPost]
        public void Post([FromBody] Match value)
        {
            matchLog.Create(value);
        }

        // PUT api/<MatchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Match value)
        {
            matchLog.Update(value);
        }

        // DELETE api/<MatchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            matchLog.Delete(id);
        }
    }
}
