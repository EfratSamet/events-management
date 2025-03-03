using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    internal interface IGuestRepository: IRepository<Guest>
    {
        List<Guest> GetGuestsByName(string guestName);
        List<Guest> GetGuestsByMail(string mail);
        List<Guest> GetGuestsByGender(Gender gender);
    }
}
