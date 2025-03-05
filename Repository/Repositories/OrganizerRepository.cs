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
    public class OrganizerRepository: IOrganizerRepository
    {
        private readonly IContext context;
        public OrganizerRepository(IContext context)
        {
            this.context = context;
        }

        public Organizer Add(Organizer item)
        {
            context.Organizers.Add(item);
            context.save();
            return item;
        }

        public void Delete(string id)
        {
            context.Organizers.Remove(Get(id));
            context.save();
        }

        public Organizer Get(string id)
        {
            return context.Organizers.FirstOrDefault(x => x.id == id);
        }

        public List<Organizer> GetAll()
        {
            return context.Organizers.ToList();
        }

        public Organizer Update(string id, Organizer item)
        {
            Organizer o = Get(id);
            o.name = item.name;
            o.mail = item.mail;
            o.password = item.password;
            context.save();
            return o;
        }
        // שאילתת חיפוש - חיפוש קבוצות לפי מארגן
        public List<Group> GetGroupsByName(string name)
        {
            return context.Groups.Where(g => g.name== name).ToList();
        }
        // שאילתת חיפוש - חיפוש אירועים לפי מארגן
        public List<Event> GetEventsByOrganizerId(string organizerId)
        {
            return context.Events.Where(e => e.organizerId == organizerId).ToList();
        }
        public Organizer GetOrganizerByMail(string mail)
        {
            return context.Organizers.FirstOrDefault(x => x.mail == mail);
        }

        public List<Group> GetGroupsByOrganizerId(string organizerId)
        {
            throw new NotImplementedException();
        }
    }
}
