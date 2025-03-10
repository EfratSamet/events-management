using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public enum Gender
    {
        female,
        male
    }
    public  class Guest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public Gender gender { get; set; }
    }
}
