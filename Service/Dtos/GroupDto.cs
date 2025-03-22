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
        public int id { get; set; }
        public string name { get; set; }
        public int organizerId { get; set; }

    }
}
