using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mock
{
    public class MyDataBase:DbContext, IContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestInEvent> GuestInEvents { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<PhotosFromEvent> PhotosFromEvents { get; set; }
        public DbSet<Seating> Seatings { get; set; }
        public DbSet<SubGuest> SubGuests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=MasterEvents;trusted_connection=true");
        }

        public void save()
        {
            SaveChanges();
        }
    }
}
