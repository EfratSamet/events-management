using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestInEvent> GuestInEvents { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<PhotosFromEvent> PhotosFromEvents { get; set; }
        public DbSet<Seating> Seatings { get; set; }
        public DbSet<SubGuest> SubGuests { get; set; }
        DatabaseFacade Database { get; } // הוספת תמיכה בטרנזקציות
        void save();

    }
}
