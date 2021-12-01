using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد نمودن نام کشور الزامیست")]
        [MaxLength(100)]
        public string Country { get; set; }

        [Required(ErrorMessage = "وارد نمودن نام شهر الزامیست")]
        [MaxLength(150)]
        public string City { get; set; }

        [Required(ErrorMessage = "وارد نمودن جزئیات آدرس الزامیست")]
        [MaxLength(500)]
        public string AddressDetail { get; set; }
        
        [ForeignKey("SubscriberId")]
        public Subscriber Subscriber { get; set; }
        public int SubscriberId { get; set; }
    }
}
