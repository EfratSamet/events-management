using Repository.Entity;
using Repository.Interfaces;

public interface ISeatingRepository : IRepository<Seating>
{
    List<int> GetSubGuestsIdsByGuestId(int guestId);


    List<int> GetSubGuestsIdsByTable(int tableNumber);

    int? GetTableByGuestId(int guestId);

    void AssignSeats(List<Seating> seatings);
}
