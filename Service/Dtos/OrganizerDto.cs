using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    [Index(nameof(mail), IsUnique = true)]
    public class OrganizerDto
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
    }
}
