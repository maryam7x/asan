using Asan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Entities
{
    public class Subscriber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="وارد نمودن نام الزامیست")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "وارد نمودن نام خانوادگی الزامیست")]
        [MaxLength(150)]
        public string LastName { get; set; }
        public ICollection<Address> AddressList { get; set; }
            = new List<Address>();
    }
}
