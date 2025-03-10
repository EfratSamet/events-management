using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    [Index(nameof(mail), IsUnique = true)]
    public class Organizer
    {
        public int ?id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public ICollection<Event> ?events { get; set; }
        public ICollection<Group> ?groups { get; set; }
    }
}
