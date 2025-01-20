using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class OrganizerDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public ICollection<Event> events { get; set; }
        public ICollection<Group> groups { get; set; }
    }
}
