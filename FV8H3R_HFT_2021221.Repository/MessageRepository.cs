using FV8H3R_HFT_2021221.Data;
using FV8H3R_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FV8H3R_HFT_2021221.Repository
{
    public class MessageRepository : IRepository<Message>, IMessageRepository
    {
        TinderDbContext ctx;

        public MessageRepository(TinderDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(Message entity)
        {
            ctx.Set<Message>().Add(entity);
            ctx.SaveChanges();
        }

        public void Delete(Message entity)
        {
            ctx.Set<Message>().Remove(entity);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(ReadOne(id));
            ctx.SaveChanges();
        }

        public IQueryable<Message> ReadAll()
        {
            return ctx.Messages.AsQueryable();
        }

        public Message ReadOne(int id)
        {
            return ReadAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(Message updated)
        {
            var msgToUpdate = ReadOne(updated.Id);

            msgToUpdate.SenderId = updated.SenderId;
            msgToUpdate.MessageSent = updated.MessageSent;
            msgToUpdate.Deleted = updated.Deleted;

            ctx.SaveChanges();
        }

        public void UnsendMsg(int id)
        {
            ReadOne(id).Deleted = true;
        }
    }
}
