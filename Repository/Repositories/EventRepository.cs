using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EventRepository : IRepository<Event>
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

        public void Delete(string id)
        {
            context.Events.Remove(Get(id));
            context.save();
        }

        public Event Get(string id)
        {
            return context.Events.FirstOrDefault(x => x.id == id);
        }

        public List<Event> GetAll()
        {
            return context.Events.ToList();
        }

        public Event Update(string id, Event item)
        {
            Event existingEvent = context.Events.FirstOrDefault(x => x.id == id);
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
    }
}
