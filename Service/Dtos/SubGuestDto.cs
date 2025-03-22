
﻿using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class SubGuestDto
    {
        public int id { get; set; }
        public int guestId { get; set; }
        public string name { get; set; }
        public Gender gender { get; set; }

    }
}
