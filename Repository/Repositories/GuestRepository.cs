using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository.Repositories
{
    public class GuestRepository:IRepository<Guest>
    {
        private readonly IContext context;
        public GuestRepository(IContext context)
        {
            this.context = context;
        }

        public Guest Add(Guest item)
        {
            context.Guests.Add(item);
            context.save();
            return item;
        }

        public void Delete(int id)
        {
            context.Guests.Remove(Get(id));
            context.save();
        }

        public Guest Get(int id)
        {
            return context.Guests.FirstOrDefault(x => x.id == id);
        }

        public List<Guest> GetAll()
        {
            return context.Guests.ToList();
        }

        public Guest Update(int id, Guest item)
        {
            var existingGuest = context.Guests.FirstOrDefault(x => x.id == id);

            existingGuest.name = item.name;
            existingGuest.mail = item.mail;
            existingGuest.gender = item.gender;

            context.save();

            return existingGuest;
        }
       //חיפוש אורחים לפי חלק מהשם
        public List<Guest> GetGuestsByName(string guestName)
        {
            return context.Guests
                .Where(g => g.name.Contains(guestName))  // מסנן לפי שם (חלקי)
                .ToList();
        }
        //חיפוש אורחים לפי מייל
        public List<Guest> GetGuestsByMail(string mail)
        {
            return context.Guests
                .Where(g => g.mail.Contains(mail))  // מסנן לפי מייל (חלקי)
                .ToList();
        }
        //חיפוש אורחים לפי מין
        public List<Guest> GetGuestsByGender(Gender gender)
        {
            return context.Guests
                .Where(g => g.gender == gender)  // מסנן לפי מין
                .ToList();
        }
    }
}
