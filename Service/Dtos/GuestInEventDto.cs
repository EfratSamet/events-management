using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class GuestInEventDto
    {
        public string id { get; set; }
        public string guestId { get; set; }
        [ForeignKey("guestId")]
        public virtual Guest guest { get; set; }
        public string eventId { get; set; }
        [ForeignKey("eventId")]
        public virtual Event event_ { get; set; }
        public bool ok { get; set; }
        public string group { get; set; }
        [ForeignKey("group")]
        public virtual Group group_ { get; set; }
    }
}
