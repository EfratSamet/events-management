using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public int organizerId { get; set; }
        [ForeignKey("organizerId")]
        public virtual Organizer organizer { get; set; }
        public int guestId { get; set; }
        [ForeignKey("guestId")]
        public virtual Guest guest { get; set; }


    }
}
