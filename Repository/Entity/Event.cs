using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class Event
    {
        public int id { get; set; }
        public int organizerId { get; set; }
        [ForeignKey("organizerId")]
        public virtual Organizer organizer { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public string address { get; set; }
        public string details { get; set; }
        public bool seperation { get; set; }
        public string invitation { get; set; }
        public ICollection<PhotosFromEvent>? photos { get; set; }
        public ICollection<GuestInEvent>? guests { get; set; }
    }
}
