using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SeatingRepository:IRepository<Seating>
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

        public void Delete(string id)
        {
            context.Seatings.Remove(Get(id));
            context.save();
        }

        public Seating Get(string id)
        {
            return context.Seatings.FirstOrDefault(x => x.id == id);
        }

        public List<Seating> GetAll()
        {
            return context.Seatings.ToList();
        }

        public Seating Update(string id, Seating item)
        {
            Seating s = Get(id);
            s.seat = item.seat;
            s.subGuestId = item.subGuestId;
            s.table = item.table;
            context.save();
            return s;
        }
       
        // חיפוש לפי מזהה אורח
        public List<SubGuest> GetSubGuestsByGuestId(string guestId)
        {
            return context.SubGuests.Where(sg => sg.guestId == guestId).ToList();
        }

        // חיפוש לפי שם (כולל חיפוש חלקי)
        public List<SubGuest> GetSubGuestsByName(string name)
        {
            return context.SubGuests.Where(sg => sg.name.Contains(name)).ToList();
        }

        // חיפוש לפי מגדר
        public List<SubGuest> GetSubGuestsByGender(Gender gender)
        {
            return context.SubGuests.Where(sg => sg.gender == gender).ToList();
        }
        // חיפוש כל האורחים לפי מספר שולחן
        public List<SubGuest> GetSubGuestsByTable(int tableNumber)
        {
            return context.Seatings
                .Where(s => s.table == tableNumber)
                .Select(s => s.subGuest)
                .ToList();
        }

        // חיפוש אורח יחיד לפי מספר שולחן ומספר כיסא
        public SubGuest GetSubGuestByTableAndSeat(int tableNumber, int seatNumber)
        {
            return context.Seatings
                .Where(s => s.table == tableNumber && s.seat == seatNumber)
                .Select(s => s.subGuest)
                .FirstOrDefault();
        }

        // חיפוש מספר שולחן לפי מזהה אורח
        public int? GetTableByGuestId(string guestId)
        {
            return context.Seatings
                .Where(s => s.subGuest.guestId == guestId)
                .Select(s => (int?)s.table)
                .FirstOrDefault();
        }


    }
}

