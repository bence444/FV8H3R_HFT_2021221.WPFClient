using FV8H3R_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Logic
{
    public interface IMatchLogic
    {
        Match ReadOne(int id);
        IList<Match> ReadAll();
        void Update(int id, Match updated);
        void Create(Match newMessage);
        void Delete(Match forDelete);
        void Delete(int id);
    }
}
