using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Delete(string id)
        {
            context.Guests.Remove(Get(id));
            context.save();
        }

        public Guest Get(string id)
        {
            return context.Guests.FirstOrDefault(x => x.id == id);
        }

        public List<Guest> GetAll()
        {
            return context.Guests.ToList();
        }

        public Guest Update(string id, Guest item)
        {
            var existingGuest = context.Guests.FirstOrDefault(x => x.id == id);

            existingGuest.name = item.name;
            existingGuest.mail = item.mail;
            existingGuest.gender = item.gender;

            context.save();

            return existingGuest;
        }

    }
}
