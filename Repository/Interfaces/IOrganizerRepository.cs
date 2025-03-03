using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
<<<<<<< HEAD
    public interface IOrganizerRepository:IRepository<Organizer>
    {
        List<Event> GetEventsByOrganizerId(string organizerId);
        List<Group> GetGroupsByOrganizerId(string organizerId);
=======
    internal interface IOrganizerRepository : IRepository<Organizer>
    {
        List<Organizer> GetOrganizersByName(string organizerName);
        List<Organizer> GetOrganizersByMail(string mail);
        List<Event> GetEventsByOrganizerId(string organizerId);
>>>>>>> d87de9640e49658aaefecde275c4738e05a9ea6c
    }
}
