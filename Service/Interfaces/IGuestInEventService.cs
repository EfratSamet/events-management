using Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGuestInEventService : IService<GuestInEventDto>
    {
        List<GuestInEventDto> GetGuestsByEventId(int eventId);
        List<GuestInEventDto> GetGuestsByEventIdOk(int eventId);
        GuestInEventDto GetGuestInEventByGuestId(int guestId);
        GuestInEventDto GetGuestInEventByGuestIdAndEventId(int guestId, int eventId);
        // נוספה פונקציה שמחלקת אורחים לשולחנות עם הפרדה לפי מגדר
        Task<Dictionary<int, List<SubGuestDto>>> AssignSubGuestsToTablesWithGenderSeparationAsync(int eventId, int seatsPerTable);
        // נוספה פונקציה שמחלקת אורחים לשולחנות בלי הפרדה לפי מגדר
        Task<Dictionary<int, List<SubGuestDto>>> AssignSubGuestsToTablesWithoutGenderSeparationAsync(int eventId, int seatsPerTable);
    }
}
