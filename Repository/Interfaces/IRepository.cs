using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T Get(string id);
        T Add(T item);
        T Update(string id, T item);
        void Delete(string id);
    }


}
