using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    internal interface IOrganizerRepository : IRepository<Organizer>
    {
        List<Organizer> GetOrganizersByName(string organizerName);
        List<Organizer> GetOrganizersByMail(string mail);
        List<Event> GetEventsByOrganizerId(string organizerId);
    }
}
