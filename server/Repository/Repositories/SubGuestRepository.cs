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
    public class SubGuestRepository : ISubGuestRepository
    {
        private readonly IContext context;

        public SubGuestRepository(IContext context)
        {
            this.context = context;
        }

        public SubGuest Add(SubGuest item)
        {
            context.SubGuests.Add(item);
            context.save();
            return item;
        }

        public void Delete(int id)
        {
            SubGuest subGuest = Get(id);
            if (subGuest != null)
            {
                context.SubGuests.Remove(subGuest);
                context.save();
            }
        }

        public SubGuest Get(int id)
        {
            return context.SubGuests.Include(sg => sg.guest).FirstOrDefault(x => x.id == id);
        }

        public List<SubGuest> GetAll()
        {
            return context.SubGuests.Include(x => x.guest).ToList();
        }

        public SubGuest Update(int id, SubGuest item)
        {
            SubGuest x = Get(id);
            if (x != null)
            {
                x.guestId = item.guestId;
                x.name = item.name;
                x.gender = item.gender;
                context.save();
            }
            return x;
        }



        // מציאת כל התת-אורחים שיש להם אותו מזהה אורח
        public List<SubGuest> GetSubGuestsByGuestId(int guestId)
        {
            return context.SubGuests
                .Where(sg => sg.guestId == guestId)
                .ToList();
        }

    
        // מציאת כל התת-אורחים לפי מין
        public List<SubGuest> GetSubGuestsByGender(Gender gender)
        {
            return context.SubGuests
                .Where(sg => sg.gender == gender)
                .ToList();
        }

        // מציאת כל התת-אורחים השייכים לאורח עם שם מסוים
        public List<SubGuest> GetSubGuestsByGuestName(string guestName)
        {
            return context.SubGuests
                .Include(sg => sg.guest)  // לוודא שהקשר ל-Guest נטען
                .Where(sg => sg.guest != null && sg.guest.name.Contains(guestName))
                .ToList();
        }

        public List<SubGuest> GetSubGuestsByEventId(int eventId)
        {
            return context.SubGuests
                .Include(sg => sg.guest)
                .Where(sg => sg.guest != null &&
                             context.GuestInEvents.Any(ge => ge.eventId == eventId && ge.guestId == sg.guestId && ge.ok))
                .ToList();
        }

        public async Task<List<SubGuest>> GetSubGuestsByGuestIdAsync(int guestId)
        {
            return await context.SubGuests
                .Where(sg => sg.guestId == guestId)
                .ToListAsync();
        }
        public async Task<List<SubGuest>> GetSubGuestsByEventIdAndGuestId(int eventId, int guestId)
        {
            return await context.SubGuests
                                .Where(sg => sg.guestId == guestId && sg.eventId == eventId)
                                .ToListAsync();  // חפש באופן אסינכרוני
        }


        public async Task<List<SubGuest>> GetSubGuestsForSeatingAsync(int guestId, int eventId)
        {
            return await context.SubGuests
                .Include(sg => sg.guest)
                .Where(sg => sg.guest != null &&
                             sg.guestId == guestId &&
                             context.GuestInEvents.Any(ge => ge.eventId == eventId && ge.guestId == guestId && ge.ok))
                .ToListAsync();
        }
    }
}
