using Service.Dtos;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IGuestInEventService : IService<GuestInEventDto>
    {
        Task<Dictionary<int, List<GuestInEventDto>>> AssignGuestsToTablesWithSubGuestsAsync(int eventId, int seatsPerTable);
        List<GuestInEventDto> GetGuestsByEventId(int eventId);
        List<GuestInEventDto> GetGuestsByEventIdOk(int eventId);
    }
}