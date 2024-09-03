using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        void Update(T entity); 
        T Get(int id);
    }
}
