using FV8H3R_HFT_2021221.Models;
using FV8H3R_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FV8H3R_HFT_2021221.Logic
{
    public class MessageLogic : IMessageLogic
    {
        IRepository<Message> msgRepo;

        public MessageLogic(IRepository<Message> msgRepo)
        {
            this.msgRepo = msgRepo;
        }

        public void Create(Message newMessage)
        {
            if (newMessage.MessageSent.Length < 1)
                throw new ArgumentException(nameof(newMessage), "Message length must be at least 1 character");

            msgRepo.Create(newMessage);
        }

        public void Delete(Message forDelete)
        {
            msgRepo.Delete(forDelete);
        }

        public void Delete(int id)
        {
            msgRepo.Delete(id);
        }

        public IList<Message> ReadAll()
        {
            return msgRepo.ReadAll().ToList();
        }

        public Message ReadOne(int id)
        {
            return msgRepo.ReadOne(id);
        }

        public void Update(int id, Message updated)
        {
            msgRepo.Update(id, updated);
        }

        public double AverageLength()
        {
            return (double)msgRepo.ReadAll().Average(x => x.MessageSent.Length);
        }
    }
}
