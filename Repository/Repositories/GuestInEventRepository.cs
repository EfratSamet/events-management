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
            var guest = Get(id);
            if (guest != null)
            {
                context.GuestInEvents.Remove(guest);
                context.save();
            }
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
            if (existingGuestInEvent == null)
                return null;

            existingGuestInEvent.guestId = item.guestId;
            existingGuestInEvent.eventId = item.eventId;
            existingGuestInEvent.group_ = item.group_;
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

        public Dictionary<string, int> GetOKCountByGroups(int eventId)
        {
            return context.GuestInEvents
                .Where(x => x.eventId == eventId && x.ok)
                .GroupBy(x => x.group_.name)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public List<GuestInEvent> GetGuestsByEventIdOk(int eventId)
        {
            return context.GuestInEvents
                .Where(x => x.eventId == eventId&& x.ok)
                .ToList();
        }

        public int CountOK(int eventId)
        {
            return context.GuestInEvents.Count(x => x.eventId == eventId && x.ok);
        }

        public int CountOKByGroup(int groupId)
        {
            return context.GuestInEvents.Count(x => x.groupId == groupId && x.ok);
        }
        public List<GuestInEvent> GetGuestsByEventAndGroupId(int eventId, int groupId)
        {
            return context.GuestInEvents
                .Where(x => x.eventId == eventId && x.groupId == groupId && x.ok)
                .ToList();
        }
        public List<GuestInEvent> GetGuestsByEventId(int eventId)
        {
            return context.GuestInEvents
                .Where(x => x.eventId == eventId )
                .ToList();
        }
    }
}
