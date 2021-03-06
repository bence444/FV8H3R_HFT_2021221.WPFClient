using FV8H3R_HFT_2021221.Data;
using FV8H3R_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FV8H3R_HFT_2021221.Repository
{
    public class MatchRepository : IRepository<Match>, IMatchRepository
    {
        TinderDbContext ctx;

        public MatchRepository(TinderDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(Match entity)
        {
            ctx.Matches.Add(entity);
            ctx.SaveChanges();
        }

        public void Delete(Match entity)
        {
            ctx.Matches.Remove(entity);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(ReadOne(id));
            ctx.SaveChanges();
        }

        public IQueryable<Match> ReadAll()
        {
            return ctx.Matches.AsQueryable();
        }

        public Match ReadOne(int id)
        {
            return ReadAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(Match updated)
        {
            var matchToUpdate = ReadOne(updated.Id);

            matchToUpdate.User_1 = updated.User_1;
            matchToUpdate.User_2 = updated.User_2;
            matchToUpdate.DeletedMatch = updated.DeletedMatch;

            ctx.SaveChanges();
        }

        public void Unmatch(int id)
        {
            ReadOne(id).DeletedMatch = true;
        }
    }
}
