using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class GroupDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string organizerId { get; set; }
        [ForeignKey("organizerId")]
        public virtual Organizer organizer { get; set; }
        public string guestId { get; set; }
        [ForeignKey("guestId")]
        public virtual Guest guest { get; set; }
    }
}
