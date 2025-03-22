
﻿using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class SeatingDto
    {
        public int id { get; set; }
        public int eventId { get; set; }
        public int subGuestId { get; set; }
        public int table { get; set; }
        public int seat { get; set; }
    }
}
