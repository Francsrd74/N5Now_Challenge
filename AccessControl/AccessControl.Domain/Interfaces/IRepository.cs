using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        public Task UpdateAsync(T entity, CancellationToken cancellationToken);
        public Task<T> GetAsync(int id, CancellationToken cancellationToken);
    }
}
