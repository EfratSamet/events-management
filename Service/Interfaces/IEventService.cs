using Repository.Entity;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEventService:IService<EventDto>
    {
       // List<GuestDto> GetGuestsByEventId(int eventId);
    }
}
