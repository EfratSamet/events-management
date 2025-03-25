using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrganizerRepository:IRepository<Organizer>
    {
        bool ExistsByEmail(string email);
    }
}
