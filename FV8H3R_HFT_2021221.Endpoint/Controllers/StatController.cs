using FV8H3R_HFT_2021221.Logic;
using FV8H3R_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        StatsLogic logic;

        public StatController(StatsLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<User> userwdm()
        {
            return logic.UsersWithDeletedMatch();
        }

        [HttpGet]
        public IEnumerable<Message> highlikes()
        {
            return logic.HighlikesByMsgId();
        }

        [HttpGet]
        public IEnumerable<Message> lastchat()
        {
            return logic.MsgsToLastMatch();
        }

        [HttpGet]
        public IEnumerable<Message> msgof()
        {
            return logic.MessageOf();
        }

        [HttpGet]
        public IEnumerable<User> issues()
        {
            return logic.UsersWithTrustIssues();
        }
    }
}
