using FV8H3R_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Logic
{
    public interface IUserLogic
    {
        User ReadOne(int id);
        IList<User> ReadAll();
        void Update(User updated);
        void Create(User newMessage);
        void Delete(User forDelete);
        void Delete(int id);
    }
}
