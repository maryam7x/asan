using Asan.Entities;
using Asan.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Services
{
    public interface ISubscriberRepository : IAsanRepository<Subscriber>
    {
        bool EntityExists(int entityId);
        public Subscriber GetById(int entityId, bool includeAddresses = false);
        public ListResponse<Subscriber> GetAll(ListRequest request);
    }
}
