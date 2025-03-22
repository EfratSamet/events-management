using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGuestRepository: IRepository<Guest>
    {
        List<Guest> GetGuestsByGroup(int groupId);
        List<Guest?> GetGuestsByEventId(int eventId);
        List<Guest> GetGuestsByOrganizerId(int organizerId);
        List<Guest> GetGuestsByName(string guestName);
        List<Guest> GetGuestsByMail(string mail);
        List<Guest> GetGuestsByGender(Gender gender);
    }
}
