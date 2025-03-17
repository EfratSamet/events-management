using Repository.Entity;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGuestService : IService<GuestDto>
    {
        void SendEmails(int eventId, string subject, string body);
        List<GuestDto> GetGuestsByGroup(int groupId);
        List<GuestDto> GetGuestsByOrganizerId(int organizerId);
        List<GuestDto> GetGuestsByEventId(int eventId);
    }
}
