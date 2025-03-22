using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class GuestInEventRepository : IGuestInEventRepository
    {
        private readonly IContext context;

        public GuestInEventRepository(IContext context)
        {
            this.context = context;
        }

        public GuestInEvent Add(GuestInEvent item)
        {
            context.GuestInEvents.Add(item);
            context.save();
            return item;
        }

        public void Delete(int id)
        {
            context.GuestInEvents.Remove(Get(id));
            context.save();
        }

        public GuestInEvent Get(int id)
        {
            return context.GuestInEvents.FirstOrDefault(x => x.id == id);
        }

        public List<GuestInEvent> GetAll()
        {
            return context.GuestInEvents.ToList();
        }

        public GuestInEvent Update(int id, GuestInEvent item)
        {
            var existingGuestInEvent = context.GuestInEvents.FirstOrDefault(x => x.id == id);

            existingGuestInEvent.guestId = item.guestId;
            existingGuestInEvent.eventId = item.eventId;
            existingGuestInEvent.ok = item.ok;

            if (item.guest != null)
            {
                existingGuestInEvent.guest = item.guest;
            }

            if (item.event_ != null)
            {
                existingGuestInEvent.event_ = item.event_;
            }


            context.save();

            return existingGuestInEvent;
        }


        public List<GuestInEvent> GuestCountOK(int eventId)
        {
            return context.GuestInEvents
                .Where(x => x.eventId == eventId && x.ok)
                .ToList();
        }

        public int CountOK(int eventId)
        {
            return context.GuestInEvents
                .Count(x => x.eventId == eventId && x.ok);
        }




    }
}
