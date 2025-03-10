using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class Seating
    {
        public int id { get; set; }
        public int eventId { get; set; }
        [ForeignKey("eventId")]
        public virtual Event event_ { get; set; }
        public int subGuestId { get; set; }
        [ForeignKey("subGuestId")]
        public virtual SubGuest subGuest { get; set; }
        public int table { get; set; }
        public int seat { get; set; }
    }
}
