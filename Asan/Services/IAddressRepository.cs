using Asan.Entities;
using Asan.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Services
{
    public interface IAddressRepository: IAsanRepository<Address>
    {
        bool EntityExists(int entityId);
        public Address GetById(int entityId);
        ListResponse<Address> GetSubscriberAddresses(AddressListRequest request);
        public Address GetSubscriberAddress(int subscriberId, int addressId);
    }
}
