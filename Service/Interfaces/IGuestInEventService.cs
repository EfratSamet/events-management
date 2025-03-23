using Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGuestInEventService : IService<GuestInEventDto>
    {
        List<GuestInEventDto> GetGuestsByEventId(int eventId);
        List<GuestInEventDto> GetGuestsByEventIdOk(int eventId);

        // נוספה פונקציה שמחלקת אורחים לשולחנות עם הפרדה לפי מגדר
        Task<Dictionary<int, List<GuestInEventDto>>> AssignGuestsToTablesWithGenderSeparationAsync(int eventId, int seatsPerTable);

        // נוספה פונקציה שמחלקת אורחים לשולחנות בלי הפרדה לפי מגדר
        Task<Dictionary<int, List<GuestInEventDto>>> AssignGuestsToTablesWithoutGenderSeparationAsync(int eventId, int seatsPerTable);
    }
}
