using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class EmailRequest
    {
        public int eventId { get; set; }
        public string ?Subject { get; set; }
        public string ?Body { get; set; }
        public string ToEmail { get; set; }
    }
}
