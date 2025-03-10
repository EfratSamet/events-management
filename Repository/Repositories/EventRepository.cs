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
            return context.Events.FirstOrDefault(x => x.id == id);
        }

        public List<Event> GetAll()
        {
            return context.Events.ToList();
        }

        public Event Update(int id, Event item)
        {
            Event existingEvent = Get(id);
            existingEvent.eventName = item.eventName;
            existingEvent.organizerId = item.organizerId;
            if(item.organizerId != null) 
                  existingEvent.organizer = item.organizer;
            existingEvent.eventDate = item.eventDate;
            existingEvent.address = item.address;
            existingEvent.details = item.details;
            existingEvent.seperation = item.seperation;
            existingEvent.invitation = item.invitation;
            existingEvent.photos = item.photos;
            existingEvent.guests = item.guests;
            context.save();
            return existingEvent; 
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
