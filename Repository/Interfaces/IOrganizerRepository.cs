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
        List<Event> GetEventsByOrganizerId(string organizerId);
        List<Group> GetGroupsByOrganizerId(string organizerId);
    }
}
