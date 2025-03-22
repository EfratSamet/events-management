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

        public void Delete(int eventId)
        {
            using (var transaction = context.Database.BeginTransaction()) // שימוש ב-context הנכון
            {
                try
                {
                    // מחיקת כל האורחים מהאירוע
                    var guestsInEvent = context.GuestInEvents.Where(g => g.eventId == eventId);
                    context.GuestInEvents.RemoveRange(guestsInEvent);
                    context.save(); // שמירת המחיקה של האורחים

                    // מחיקת האירוע עצמו
                    var eventToDelete = context.Events.Find(eventId);
                    if (eventToDelete != null)
                    {
                        context.Events.Remove(eventToDelete);
                        context.save(); // שמירת המחיקה של האירוע
                    }

                    transaction.Commit(); // אישור השינויים
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // ביטול במקרה של שגיאה
                    throw new Exception("שגיאה במחיקת האירוע", ex);
                }
            }
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
                });
            }
            context.save();
            return existingEvent;
        }

        public List<Guest> GetGuestsByEventId(int eventId)
        {
            // קריאת הנתונים מתוך מסד הנתונים והקרנה למבנה של אורחים בלבד
            var result = context.GuestInEvents
                .Where(ge => ge.eventId == eventId)
                .Select(ge => ge.guest) // בוחר את האורח בלבד
                .ToList(); // מבצע את השאילתה ומעביר את התוצאות לזיכרון

            if (result == null || result.Count == 0)
                throw new Exception($"Event with id {eventId} not found.");

            return result;
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
