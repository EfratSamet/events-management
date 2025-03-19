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
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MasterEvents;Trusted_Connection=True;");
        }

        public void save()
        {
            SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestInEvent>()
                .HasOne(g => g.event_)
                .WithMany(e => e.guests)
                .HasForeignKey(g => g.eventId)
                .OnDelete(DeleteBehavior.Restrict); // במקום Cascade

            modelBuilder.Entity<GuestInEvent>()
                .HasOne(g => g.group_)
                .WithMany()
                .HasForeignKey(g => g.groupId)
                .OnDelete(DeleteBehavior.Restrict); // במקום Cascade

            modelBuilder.Entity<GuestInEvent>()
                .HasOne(g => g.guest)
                .WithMany()
                .HasForeignKey(g => g.guestId)
                .OnDelete(DeleteBehavior.Restrict); // במקום Cascade

            base.OnModelCreating(modelBuilder);
        }

    }
}
