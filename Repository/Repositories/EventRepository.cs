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
    public class EventRepository : IEventRepository
    {
        private readonly IContext context;
        public EventRepository(IContext context)
        {
            this.context = context;
        }

        public Event Add(Event item)
        {
            context.Events.Add(item);
            context.save();
            return item;
        }

        public void Delete(int id)
        {
            context.Events.Remove(Get(id));
            context.save();
        }

        public Event Get(int id)
        {
            return context.Events
                .FirstOrDefault(x => x.id == id);
        }

        public List<Event> GetAll()
        {
            return context.Events.Include(g=>g.guests).ThenInclude(g => g.guest) // טוען גם את פרטי האורח עצמו
.ToList();
        }

        public Event Update(int id, Event item)
        {
            Event existingEvent = Get(id);

            if (existingEvent == null)
                throw new Exception($"Event with id {id} not found.");

            existingEvent.eventName = item.eventName;
            existingEvent.organizerId = item.organizerId;
            existingEvent.eventDate = item.eventDate;
            existingEvent.address = item.address;
            existingEvent.details = item.details;
            existingEvent.seperation = item.seperation;
            existingEvent.invitation = item.invitation;

            // לוודא שהרשימה קיימת
            existingEvent.guests ??= new List<GuestInEvent>();
            // מחיקת אורחים קיימים והוספת החדשים
            existingEvent.guests.Clear();
            foreach (var guest in item.guests ?? new List<GuestInEvent>())
            {
                existingEvent.guests.Add(new GuestInEvent
                {
                    guestId = guest.guestId,
                    eventId = guest.eventId,
                    ok = guest.ok,
                    groupId = guest.groupId
                });
            }
            context.save();
            return existingEvent;
        }

        public List<GuestInEvent> GetGuestsByEventId(int eventId)
        {
            var eventEntity = context.Events
                .Include(e => e.guests)  // טוען את האורחים של האירוע
                .FirstOrDefault(e => e.id == eventId);

            if (eventEntity == null)
                throw new Exception($"Event with id {eventId} not found.");

            return eventEntity.guests.ToList();  // מחזיר את רשימת האורחים של האירוע
        }


        //חיפוש אירועים לפי מארגן
        public List<Event> GetEventsByOrganizerId(int organizerId)
        {
            return context.Events.Where(e => e.organizerId == organizerId).ToList();
        }
        //חיפוש אירועים לפי טווח תאריכים (תאריך התחלה וסיום)
        public List<Event> GetEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            return context.Events.Where(e => e.eventDate >= startDate && e.eventDate <= endDate).ToList();
        }
        //חיפוש אירועים לפי מיקום
        public List<Event> GetEventsByAddress(string address)
        {
            return context.Events
                .Where(e => e.address.Contains(address))
                .ToList();
        }
        //חיפוש אירועים עתידיים
        public List<Event> GetUpcomingEvents()
        {
            return context.Events
                .Where(e => e.eventDate > DateTime.Now)
                .OrderBy(e => e.eventDate)
                .ToList();
        }
        //חיפוש אירועים לפי מילת מפתח בכתובת
        public List<Event> GetEventsByAddressKeyword(string keyword)
        {
            return context.Events
                .Where(e => e.address.Contains(keyword))
                .ToList();
        }
    }
}
