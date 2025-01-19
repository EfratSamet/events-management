using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class GroupDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string organizerId { get; set; }
        public string guestId { get; set; }
    }
}
