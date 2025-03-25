using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository.Repositories
{
    public class GuestRepository : IGuestRepository
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

        public List<Guest> GetGuestsByGroup(int groupId)
        {
            return context.Guests
                .Where(g => g.groupId == groupId)
                .ToList();
        }

        public List<Guest> GetGuestsByOrganizerId(int organizerId)
        {
            return context.Guests
                .Where(g => context.GuestInEvents
                    .Any(ge => context.Events
                        .Any(e => e.organizerId == organizerId && e.id == ge.eventId) && ge.guestId == g.id))
                .Distinct()
                .ToList();
        }
        public List<Guest?> GetGuestsByEventId(int eventId)
        {
            return context.GuestInEvents
                .Where(ge => ge.eventId == eventId)
                .Select(ge => ge.guest)
                .Distinct()
                .ToList();
        }



    }
}
