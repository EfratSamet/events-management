using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISeatingService:IService<SeatingDto>
    {
        List<int> GetSubGuestsIdsByGuestId(int guestId);
        List<int> GetSubGuestsIdsByTable(int tableNumber);
        int? GetTableByGuestId(int guestId);
        void AssignSeats(List<SeatingDto> seatings);
    }
}
