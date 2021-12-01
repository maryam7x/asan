using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asan.Models
{
    public class SubscriberQueryDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<AddressQueryDto> AddressList { get; set; }
            = new List<AddressQueryDto>();
    }
}
