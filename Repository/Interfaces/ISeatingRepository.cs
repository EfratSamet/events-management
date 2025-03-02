using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    internal interface ISeatingRepository : IRepository<Seating>
    {
        List<SubGuest> GetSubGuestsByGuestId(string guestId);
        List<SubGuest> GetSubGuestsByName(string name);
        List<SubGuest> GetSubGuestsByGender(Gender gender);
        List<SubGuest> GetSubGuestsByTable(int tableNumber);
        SubGuest GetSubGuestByTableAndSeat(int tableNumber, int seatNumber);
        int? GetTableByGuestId(string guestId);
    }
}
