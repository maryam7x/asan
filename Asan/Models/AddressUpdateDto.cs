using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class AddressUpdateDto
    {
        [Required(ErrorMessage ="وارد نمودن شناسه الزامیست")]
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد نمودن نام کشور الزامیست")]
        [MaxLength(100)]
        public string Country { get; set; }

        [Required(ErrorMessage = "وارد نمودن نام شهر الزامیست")]
        [MaxLength(150)]
        public string City { get; set; }

        [Required(ErrorMessage = "وارد نمودن جزئیات آدرس الزامیست")]
        [MaxLength(500)]
        [MinLength(3)]
        public string AddressDetail { get; set; }
    }
}
