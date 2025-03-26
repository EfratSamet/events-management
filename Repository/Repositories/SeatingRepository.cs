using Repository.Entity;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class SeatingRepository : ISeatingRepository
    {
        private readonly IContext context;

        public SeatingRepository(IContext context)
        {
            this.context = context;
        }

        public Seating Add(Seating item)
        {
            context.Seatings.Add(item);
            context.save();
            return item;
        }

        public void Delete(int id)
        {
            var seating = Get(id);
            if (seating != null)
            {
                context.Seatings.Remove(seating);
            }
        }

        public Seating Get(int id)
        {
            return context.Seatings
                .Include(s => s.subGuest) // טעינת תת-אורח אם יש קשר
                .FirstOrDefault(x => x.id == id);
        }

        public List<Seating> GetAll()
        {
            return context.Seatings.ToList();

        }


        public Seating Update(int id, Seating item)
        {
            var seating = Get(id);
            if (seating != null)
            {
                seating.seat = item.seat;
                seating.subGuestId = item.subGuestId;
                seating.table = item.table;
            }
            return seating;
        }

        // ✅ חיפוש כל תתי האורחים לפי מזהה אורח
        public List<int> GetSubGuestsIdsByGuestId(int guestId)
        {
            return context.Seatings
                .Where(s => s.subGuest.guestId == guestId) // סינון לפי מזהה אורח
                .Select(s => s.subGuest.guestId) // מחזיר את המזהה של האורח בלבד
                .ToList();
        }

        // ✅ מחזיר את כל תתי-האורחים שיושבים בשולחן מסוים, רק את המזהים שלהם
        public List<int> GetSubGuestsIdsByTable(int tableNumber)
        {
            return context.Seatings
                .Where(s => s.table == tableNumber) // סינון לפי מספר שולחן
                .Select(s => s.subGuest.guestId) // מחזיר את המזהה של האורח בלבד
                .ToList();
        }

        // ✅ חיפוש מספר שולחן לפי מזהה אורח
        public int? GetTableByGuestId(int guestId)
        {
            return context.Seatings
                .Where(s => s.subGuest.guestId == guestId)
                .Select(s => (int?)s.table) // מחזיר את מספר השולחן
                .FirstOrDefault();
        }

        // ✅ שמירת סידור מקומות ישיבה בבסיס הנתונים
        public void AssignSeats(List<Seating> seatings)
        {
            context.Seatings.AddRange(seatings);
        }
    }
}
