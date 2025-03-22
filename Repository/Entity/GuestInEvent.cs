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
        public int id { get; set; }
        public int guestId { get; set; }
        [ForeignKey("guestId")]
        public virtual Guest? guest { get; set; }
        public int eventId { get; set; }
        [ForeignKey("eventId")]
        public virtual Event ?event_ { get; set; }
        public bool ok { get; set; }
        //public int groupId { get; set; }
        //[ForeignKey("groupId")]
        //public virtual Group ?group_ { get; set; }
    }
}
