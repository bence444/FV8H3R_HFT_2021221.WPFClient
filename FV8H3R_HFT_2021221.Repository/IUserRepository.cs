using FV8H3R_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        void ChangeName(int id, string text);
        void ChangeBio(int id, string text);
        void RefreshLikes(int id);
    }
}
