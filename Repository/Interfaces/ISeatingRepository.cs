using Repository.Entity;
using Repository.Interfaces;

internal interface ISeatingRepository : IRepository<Seating>
{
    List<SubGuest> GetSubGuestsByGuestId(int guestId); // שונה מ-string ל-int
    List<SubGuest> GetSubGuestsByName(string name);
    List<SubGuest> GetSubGuestsByGender(Gender gender);
    List<SubGuest> GetSubGuestsByTable(int tableNumber);
    SubGuest GetSubGuestByTableAndSeat(int tableNumber, int seatNumber);
    int? GetTableByGuestId(int guestId); // שונה גם כאן אם צריך
}
