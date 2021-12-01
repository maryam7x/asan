using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asan.Models
{
    public class AddressQueryDto : LinkResourceBaseDto
    {
        public int Id { get; set; }
              
        public string Country { get; set; }

        public string City { get; set; }
               
        public string AddressDetail { get; set; }

        public int SubscriberId { get; set; }
    }
}
