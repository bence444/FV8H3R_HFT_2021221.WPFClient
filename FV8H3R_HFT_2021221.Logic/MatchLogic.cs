using FV8H3R_HFT_2021221.Models;
using FV8H3R_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Logic
{
    public class MatchLogic : IMatchLogic
    {
        IRepository<Match> matchRepo;

        public MatchLogic(IRepository<Match> matchRepo)
        {
            this.matchRepo = matchRepo;
        }

        public void Create(Match newMessage)
        {
            matchRepo.Create(newMessage);
        }

        public void Delete(Match forDelete)
        {
            matchRepo.Delete(forDelete);
        }

        public void Delete(int id)
        {
            matchRepo.Delete(id);
        }

        public IList<Match> ReadAll()
        {
            return matchRepo.ReadAll().ToList();
        }

        public Match ReadOne(int id)
        {
            return matchRepo.ReadOne(id);
        }

        public void Update(int id, Match updated)
        {
            matchRepo.Update(id, updated);
        }
    }
}
