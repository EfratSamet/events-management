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
    public class MyDataBase : DbContext, IContext
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
            optionsBuilder.UseSqlServer("Server=localhost;Database=MasterEvents;Trusted_Connection=True;");
        }

        public void save()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seating>()
          .HasOne(s => s.event_)
          .WithMany()
          .HasForeignKey(s => s.eventId)
          .OnDelete(DeleteBehavior.Cascade); // השאר CASCADE עבור מחיקת Event

            modelBuilder.Entity<Seating>()
                .HasOne(s => s.subGuest)
                .WithMany()
                .HasForeignKey(s => s.subGuestId)
                .OnDelete(DeleteBehavior.Restrict); // שים Restrict כדי למנוע מחיקה מעגלית

            modelBuilder.Entity<SubGuest>()
                .HasOne(sg => sg.guest)
                .WithMany()
                .HasForeignKey(sg => sg.guestId)
                .OnDelete(DeleteBehavior.Restrict); // השאר Restrict למניעת מחיקה של Guests
          
            modelBuilder.Entity<GuestInEvent>()
                .HasOne(g => g.guest)
                .WithMany()
                .HasForeignKey(g => g.guestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhotosFromEvent>()
                .HasOne(p => p.event_)
                .WithMany(e => e.photos)
                .HasForeignKey(p => p.eventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhotosFromEvent>()
                .HasOne(p => p.guest)
                .WithMany()
                .HasForeignKey(p => p.guestId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);

        }
    }
}
