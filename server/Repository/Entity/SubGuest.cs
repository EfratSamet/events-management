using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    
    public class SubGuest
    {
        public int id { get; set; }
        public int guestId { get; set; }
        [ForeignKey("guestId")]
        public virtual Guest guest { get; set; }
        public int eventId { get; set; }
        [ForeignKey("eventId")]
        public virtual Event Event { get; set; }
        public string name { get; set; }
        public Gender gender { get; set; }

    }
}
