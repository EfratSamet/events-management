using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository.Repositories
{
    public class GuestRepository:IGuestRepository
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

        public void Delete(int id)
        {
            context.Guests.Remove(Get(id));
            context.save();
        }

        public Guest Get(int id)
        {
            return context.Guests.FirstOrDefault(x => x.id == id);
        }

        public List<Guest> GetAll()
        {
            return context.Guests.ToList();
        }

        public Guest Update(int id, Guest item)
        {
            var existingGuest = context.Guests.FirstOrDefault(x => x.id == id);

            existingGuest.name = item.name;
            existingGuest.mail = item.mail;
            existingGuest.gender = item.gender;

            context.save();

            return existingGuest;
        }
        public void SendEmails(int eventId, string subject, string body)
        {
            List<Guest> guests = GetGuestsByEventId(eventId);
            string smtpServer = "smtp.gmail.com"; // שרת SMTP של הספק שלך
            int smtpPort = 587; // פורט SMTP (לרוב 587 או 465)
            string senderEmail = "sametfamily21@gmail.com";
            string senderPassword = "kcau xvtz srta iytm\r\n";
            Console.WriteLine("Starting the email sending process...");

            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
            {
                // הגדרת פרטי החיבור ל-SMTP
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.EnableSsl = true; // השתמש ב-SSL כדי לאבטח את החיבור

                // עבור כל אורח ברשימה
                foreach (var guest in guests)
                {
                    Console.WriteLine($"Preparing to send email to {guest.mail}...");

                    try
                    {
                        // יצירת מייל חדש
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress(senderEmail),
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true // אם הגוף של המייל הוא HTML
                        };

                        // הוספת כתובת המייל של הנמען
                        mail.To.Add(guest.mail);

                        // שליחת המייל
                        Console.WriteLine($"Sending email to {guest.mail}...");
                        client.Send(mail);

                        Console.WriteLine($"Email sent successfully to {guest.mail}");
                    }
                    catch (Exception ex)
                    {
                        // הדפסת הודעת שגיאה אם משהו השתבש
                        Console.WriteLine($"Failed to send email to {guest.mail}: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Email sending process completed.");
        }
        public List<Guest> GetGuestsByGroup(int groupId)
        {
            return context.Guests
                .Where(g => g.groupId == groupId)    
                .ToList();
        }

        public List<Guest> GetGuestsByOrganizerId(int organizerId)
        {
            return context.Guests
                .Where(g => context.GuestInEvents
                    .Any(ge => context.Events
                        .Any(e => e.organizerId == organizerId && e.id == ge.eventId) && ge.guestId == g.id))
                .Distinct()
                .ToList();
        }
        public List<Guest?> GetGuestsByEventId(int eventId)
        {
            return context.GuestInEvents
                .Where(ge => ge.eventId == eventId)
                .Select(ge => ge.guest)
                .Distinct()
                .ToList();
        }
        //חיפוש אורחים לפי חלק מהשם
        public List<Guest> GetGuestsByName(string guestName)
        {
            return context.Guests
                .Where(g => g.name.Contains(guestName))  // מסנן לפי שם (חלקי)
                .ToList();
        }
        //חיפוש אורחים לפי מייל
        public List<Guest> GetGuestsByMail(string mail)
        {
            return context.Guests
                .Where(g => g.mail.Contains(mail))  // מסנן לפי מייל (חלקי)
                .ToList();
        }
        //חיפוש אורחים לפי מין
        public List<Guest> GetGuestsByGender(Gender gender)
        {
            return context.Guests
                .Where(g => g.gender == gender)  // מסנן לפי מין
                .ToList();
        }
    }
}
