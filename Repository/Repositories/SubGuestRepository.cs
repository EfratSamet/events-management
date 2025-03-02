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
    public class SubGuestRepository:IRepository<SubGuest>
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

        public void Delete(string id)
        {
            context.SubGuests.Remove(Get(id));
            context.save();
        }

        public SubGuest Get(string id)
        {
            return context.SubGuests.FirstOrDefault(x => x.id == id);
        }

        public List<SubGuest> GetAll()
        {
            return context.SubGuests.Include(x=>x.guest).ToList();
        }

        public SubGuest Update(string id, SubGuest item)
        {
            SubGuest x = Get(id);
            x.guestId = item.guestId;
            x.name = item.name;
            x.gender = item.gender;
            context.save();
            return x;
        }
        //חיפוש לפי חלק מהשם
        public List<SubGuest> GetSubGuestsByName(string subGuestName)
        {
            return context.SubGuests
                .Where(sg => sg.name.Contains(subGuestName))  // מסנן לפי שם (חלקי)
                .ToList();
        }
        // מציאת כל התת-אורחים שיש להם אותו מזהה אורח
        public List<SubGuest> GetSubGuestsByGuestId(string guestId)
        {
            return context.SubGuests.Where(sg => sg.guestId == guestId).ToList();
        }

        // מציאת תת-אורחים לפי מזהה אורח ומגדר
        public List<SubGuest> GetSubGuestsByGuestIdAndGender(string guestId, Gender gender)
        {
            return context.SubGuests.Where(sg => sg.guestId == guestId && sg.gender == gender).ToList();
        }

        // מציאת כל התת-אורחים לפי מין
        public List<SubGuest> GetSubGuestsByGender(Gender gender)
        {
            return context.SubGuests.Where(sg => sg.gender == gender).ToList();
        }

        // מציאת כל התת-אורחים השייכים לאורח עם שם מסוים
        public List<SubGuest> GetSubGuestsByGuestName(string guestName)
        {
            return context.SubGuests
                .Where(sg => sg.guest != null && sg.guest.name.Contains(guestName))
                .ToList();
        }

    }
}
