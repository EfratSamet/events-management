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
    }
}
