using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IGuestInEventRepository : IRepository<GuestInEvent>
    {
        Dictionary<string, int> GetOKCountByGroups(int eventId);
        int CountOK(int eventId);
        int CountOKByGroup(int groupId);
        List<GuestInEvent> GetGuestsByEventId(int eventId);
        List<GuestInEvent> GetGuestsByEventAndGroupId(int eventId, int groupId);
        List<GuestInEvent> GetGuestsByEventIdOk(int eventId);

    }
}
