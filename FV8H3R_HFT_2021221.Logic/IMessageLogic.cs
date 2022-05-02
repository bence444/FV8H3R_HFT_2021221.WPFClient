using FV8H3R_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Logic
{
    public interface IMessageLogic
    {
        Message ReadOne(int id);
        IList<Message> ReadAll();
        void Update(Message updated);
        void Create(Message newMessage);
        void Delete(Message forDelete);
        void Delete(int id);
    }
}
