using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Service.Services
{
    public class MailjetService : IMailjetService
    {
        private const string ApiKey = "8efb34610befbe31c810ddc0d25d939c";
        private const string ApiSecret = "7270a0984c8ac854aedd3157428523c9";

        private readonly IGuestService _guestService; // תלות ב-GuestService

        public MailjetService(IGuestService guestService)
        {
            this._guestService = guestService;
        }

        public async Task SendEmailAsync(int eventId, string subject, string body)
        {
            // קבלת רשימת המיילים מהפונקציה GetGuestsByEventId
            var guests = _guestService.GetGuestsByEventId(eventId);

            // יצירת אובייקט MailjetClient
            MailjetClient client = new MailjetClient(ApiKey, ApiSecret);

            // יצירת הרשימה של נמענים
            var toEmails = new JArray();
            foreach (var guest in guests)
            {
                if (guest?.mail != null) // לוודא שהמייל לא null
                {
                    toEmails.Add(new JObject
                {
                    { "Email", guest.mail } // הוספת כתובת המייל
                });
                }
            }

            // יצירת בקשה לשליחת מייל
            var request = new MailjetRequest { Resource = SendV31.Resource }
                .Property(Send.Messages, new JArray
                {
                new JObject
                {
                    { "From", new JObject
                        {
                            { "Email", "sametfamily21@gmail.com" },
                            { "Name", "efrat samet" }
                        }
                    },
                    { "To", toEmails },
                    { "Subject", subject },
                    { "TextPart", body },
                    { "HTMLPart", $"<h3>{body}</h3>" }
                }
                });

            // שליחת הבקשה
            MailjetResponse response = await client.PostAsync(request);

            // בדיקת תוצאה
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Emails sent successfully!");
            }
            else
            {
                Console.WriteLine($"Failed to send emails. Status: {response.StatusCode}, Error: {response.GetErrorMessage()}");
            }
        }
    }


}
