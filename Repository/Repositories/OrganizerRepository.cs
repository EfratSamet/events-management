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

        public void Delete(int id)
        {
            context.Organizers.Remove(Get(id));
            context.save();
        }

        public Organizer Get(int id)
        {
            return context.Organizers.FirstOrDefault(x => x.id == id);
        }

        public List<Organizer> GetAll()
        {
            return context.Organizers.ToList();
        }

        public Organizer Update(int id, Organizer item)
        {
            Organizer o = Get(id);
            o.name = item.name;
            o.mail = item.mail;
            o.password = item.password;
            context.save();
            return o;
        }


        public bool ExistsByEmail(string email)
        {
            return context.Organizers.Any(x=>x.mail == email);
        }
    }
}
