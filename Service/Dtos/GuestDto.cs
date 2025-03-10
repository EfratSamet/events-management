using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class GuestDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public Gender gender { get; set; }
    }
}
