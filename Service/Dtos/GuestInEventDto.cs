using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class GuestInEventDto
    {
        public string id { get; set; }
        public string guestId { get; set; }
        public string eventId { get; set; }
        public bool ok { get; set; }
        public Category category { get; set; }
    }
}
