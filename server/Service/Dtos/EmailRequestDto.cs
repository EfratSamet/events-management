using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class EmailRequestDto
    {
        public int eventId { get; set; }
        public string ?Subject { get; set; }
        public string ?Body { get; set; }
        public string ToEmail { get; set; }
    }
}
