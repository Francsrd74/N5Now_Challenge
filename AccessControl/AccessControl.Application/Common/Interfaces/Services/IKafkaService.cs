using AccessControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Application.Common.Interfaces.Services
{
    public interface IKafkaService<out TSource> where TSource : class
    {
        public Task<TSource> ProducerTopicAsync<TSource>(TSource entity, CancellationToken cancellationToken);
    }
}
