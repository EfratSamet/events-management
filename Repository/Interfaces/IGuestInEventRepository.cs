using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IGuestInEventRepository : IRepository<GuestInEvent>
    {
        int CountOK(int eventId);
        int CountOKByGroup(int groupId);
        List<GuestInEvent> GetGuestsByEventId(int eventId);
        List<GuestInEvent> GetGuestsByEventAndGroupId(int eventId, int groupId);
        List<GuestInEvent> GetGuestsByEventIdOK(int eventId);
        GuestInEvent GetGuestInEventByGuestId(int guestId);
    }
}