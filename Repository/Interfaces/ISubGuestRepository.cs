using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISubGuestRepository : IRepository<SubGuest>
    {
        List<SubGuest> GetSubGuestsByName(string subGuestName);
        List<SubGuest> GetSubGuestsByGuestId(int guestId);
        List<SubGuest> GetSubGuestsByGuestIdAndGender(int guestId, Gender gender);
        List<SubGuest> GetSubGuestsByGender(Gender gender);
        List<SubGuest> GetSubGuestsByGuestName(string guestName);
        List<SubGuest> GetSubGuestsByEventId(int eventId);

        // פונקציה אסינכרונית להחזרת כל תתי האורחים לפי מזהה אורח
        Task<List<SubGuest>> GetSubGuestsByGuestIdAsync(int guestId);

        // פונקציה להחזרת כל תתי האורחים של אורחים שהגיעו לאירוע
        Task<List<SubGuest>> GetSubGuestsByEventIdAsync(int eventId);

        // פונקציה להחזרת כל תתי האורחים של אורח מסוים, רק אם הוא אישר הגעה לאירוע
        Task<List<SubGuest>> GetSubGuestsForSeatingAsync(int guestId, int eventId);
    }
}
