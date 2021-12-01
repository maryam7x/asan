using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class SubscriberCreateDto
    {
        [Required(ErrorMessage = "وارد نمودن نام الزامیست")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "وارد نمودن نام خانوادگی الزامیست")]
        [MaxLength(150)]
        public string LastName { get; set; }
    }
}
