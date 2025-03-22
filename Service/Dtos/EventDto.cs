using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class EventDto
    {
        public int id { get; set; }
        public int organizerId { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public string address { get; set; }
        public string details { get; set; }
        public bool seperation { get; set; }
        public string invitation { get; set; }
       
    }
}
