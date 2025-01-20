using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class GuestInEvent
    {
        public string id { get; set; }
        public string guestId { get; set; }
        [ForeignKey("guestId")]
        public virtual Guest guest { get; set; }
        public string eventId { get; set; }
        [ForeignKey("eventId")]
        public virtual Event event_ { get; set; }
        public bool ok { get; set; }
        public Category category { get; set; }
    }
}
