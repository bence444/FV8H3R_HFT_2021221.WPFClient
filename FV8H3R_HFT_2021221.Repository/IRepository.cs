using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FV8H3R_HFT_2021221.Models;

namespace FV8H3R_HFT_2021221.Repository
{
    public interface IRepository<T> where T : class
    {
        T ReadOne(int id);
        IQueryable<T> ReadAll();

        void Delete(T entity);
        void Delete(int id);
        void Create(T entity);
        void Update(T updated);
    }
}
