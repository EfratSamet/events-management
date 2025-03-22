using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interfaces;
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
                context.save();
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
            return context.Seatings.Include(s => s.subGuest).ToList();
        }

        public Seating Update(int id, Seating item)
        {
            var seating = Get(id);
            if (seating != null)
            {
                seating.seat = item.seat;
                seating.subGuestId = item.subGuestId;
                seating.table = item.table;
                context.save();
            }
            return seating;
        }

        // ✅ חיפוש כל תתי האורחים לפי מזהה אורח
        public List<SubGuest> GetSubGuestsByGuestId(int guestId)
        {
            return context.Seatings
                .Where(s => s.subGuest.guestId == guestId)
                .Select(s => s.subGuest)
                .ToList();
        }

        // ✅ חיפוש כל תתי האורחים לפי שם (כולל חיפוש חלקי)
        public List<SubGuest> GetSubGuestsByName(string name)
        {
            return context.Seatings
                .Where(s => s.subGuest.name.Contains(name))
                .Select(s => s.subGuest)
                .ToList();
        }

        // ✅ חיפוש כל תתי האורחים לפי מגדר
        public List<SubGuest> GetSubGuestsByGender(Gender gender)
        {
            return context.Seatings
                .Where(s => s.subGuest.gender == gender)
                .Select(s => s.subGuest)
                .ToList();
        }

        // ✅ מחזיר את כל תתי-האורחים שיושבים בשולחן מסוים
        public List<SubGuest> GetSubGuestsByTable(int tableNumber)
        {
            return context.Seatings
                .Where(s => s.table == tableNumber)
                .Select(s => s.subGuest) // מחזיר את רשימת תתי-האורחים עצמם
                .ToList();
        }


        // ✅ חיפוש תת-אורח לפי מספר שולחן ומספר כיסא
        public SubGuest GetSubGuestByTableAndSeat(int tableNumber, int seatNumber)
        {
            return context.Seatings
                .Where(s => s.table == tableNumber && s.seat == seatNumber)
                .Select(s => s.subGuest) // מחזיר את תת-האורח עצמו
                .FirstOrDefault();
        }


        // ✅ חיפוש מספר שולחן לפי מזהה אורח
        public int? GetTableByGuestId(int guestId)
        {
            return context.Seatings
                .Where(s => s.subGuest.guestId == guestId)
                .Select(s => (int?)s.table)
                .FirstOrDefault();
        }

        // ✅ שמירת סידור מקומות ישיבה בבסיס הנתונים
        public void AssignSeats(List<Seating> seatings)
        {
            context.Seatings.AddRange(seatings);
            context.save();
        }
    }
}
