using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IGuestInEventRepository : IRepository<GuestInEvent>
    {
        List<GuestInEvent> GetGuestsByEventId(int eventId);
        List<GuestInEvent> GetGuestsByEventIdOK(int eventId);
        GuestInEvent GetGuestInEventByGuestId(int guestId);
        GuestInEvent GetGuestInEventByGuestIdAndEventId(int guestId, int eventId);
    }
}