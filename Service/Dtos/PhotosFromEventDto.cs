using Microsoft.AspNetCore.Http;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class PhotosFromEventDto
    {
        public string id { get; set; }
        public string guestId { get; set; }
        [ForeignKey("guestId")]
        public virtual Guest guest { get; set; }
        public string eventId { get; set; }
        [ForeignKey("eventId")]
        public virtual Event event_ { get; set; }
        public string imageUrl { get; set; }
        public string blessing { get; set; }
    }
}
