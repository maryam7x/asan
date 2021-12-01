using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Linkdto
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
        public Linkdto()
        {
        }
        public Linkdto(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }    
}
