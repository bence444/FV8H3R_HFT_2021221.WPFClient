using FV8H3R_HFT_2021221.Models;
using FV8H3R_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Logic
{
    public class StatsLogic
    {
        IRepository<User> userRepo;
        IRepository<Match> matchRepo;
        IRepository<Message> msgRepo;

        public StatsLogic(IRepository<User> userRepo, IRepository<Match> matchRepo, IRepository<Message> msgRepo)
        {
            this.userRepo = userRepo;
            this.matchRepo = matchRepo;
            this.msgRepo = msgRepo;
        }

        public IEnumerable<User> UsersWithDeletedMatch()
        {
            var ur = from user in userRepo.ReadAll().ToList()
                     join match in matchRepo.ReadAll().ToList() on user.Id equals match.User_1
                     where match.DeletedMatch
                     select user;

            var ur2 = from user in userRepo.ReadAll().ToList()
                      join match in matchRepo.ReadAll().ToList() on user.Id equals match.User_2
                      where match.DeletedMatch
                      select user;

            return ur.Concat(ur2);
        }

        public IEnumerable<Message> HighlikesByMsgId()
        {
            var um = from msg in msgRepo.ReadAll().ToList()
                     join user in userRepo.ReadAll().ToList() on msg.SenderId equals user.Id
                     where user.AvailableLikes > 10
                     orderby msg.Id descending
                     select msg;

            return um;
        }

        public IEnumerable<Message> MsgsToLastMatch()
        {
            var mm = from msg in msgRepo.ReadAll().ToList()
                     join match in matchRepo.ReadAll().ToList() on msg.MatchId equals match.Id
                     where msg.MatchId.Equals(matchRepo.ReadAll().ToList().Last().Id)
                     select msg;

            return mm;
        }

        public IEnumerable<Message> MessageOf()
        {
            var mo = from msg in msgRepo.ReadAll().ToList()
                     join user in userRepo.ReadAll().ToList() on msg.SenderId equals user.Id
                     where user.Name.Contains('á')
                     select msg;

            return mo;
        }

        public IEnumerable<User> UsersWithTrustIssues()
        {
            var to = (from user in userRepo.ReadAll().ToList()
                     join msg in msgRepo.ReadAll().ToList() on user.Id equals msg.SenderId
                     where msg.Deleted
                     select user).Distinct();

            return to;
        }
    }
}
