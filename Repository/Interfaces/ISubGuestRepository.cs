using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    internal interface ISubGuestRepository : IRepository<SubGuest>
    {
        List<SubGuest> GetSubGuestsByName(string subGuestName);
        List<SubGuest> GetSubGuestsByGuestId(string guestId);
        List<SubGuest> GetSubGuestsByGuestIdAndGender(string guestId, Gender gender);
        List<SubGuest> GetSubGuestsByGender(Gender gender);
        List<SubGuest> GetSubGuestsByGuestName(string guestName);
    }
}
