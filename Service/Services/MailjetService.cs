using System;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Service.Services
{
    public class MailjetService : IMailjetService
    {
        private const string ApiKey = "2fa9a9028f4fb3cf1b82ab5e7a9c33ca";
        private const string ApiSecret = "65f6f478a3d83368f4b04c426517f76c";

        private readonly IGuestService _guestService;

        public MailjetService(IGuestService guestService)
        {
            _guestService = guestService;
        }

        //public async Task SendEmailAsync(int eventId, string subject, string body)
        //{
        //    var guests = _guestService.GetGuestsByEventId(eventId);
        //    Console.WriteLine("1 - " + eventId);
        //    if (guests == null || guests.Count == 0)
        //    {
        //        Console.WriteLine("No guests found for the event.");
        //        return;
        //    }

        //    var toEmails = new JArray();
        //    foreach (var guest in guests)
        //    {
        //        Console.WriteLine("2 - " +guest);
        //        if (!string.IsNullOrEmpty(guest?.mail))
        //        {
        //            toEmails.Add(new JObject { { "Email", guest.mail } });
        //        }
        //    }

        //    if (toEmails.Count == 0)
        //    {
        //        Console.WriteLine("No valid email addresses found.");
        //        return;
        //    }

        //    var request = CreateEmailRequest(toEmails, subject, body);
        //    await SendMailjetRequest(request);
        //    Console.WriteLine("שלח את המייל ");
        //}

        public async Task SendSingleEmailAsync(string toEmail, string subject, string body)
        {
            if (string.IsNullOrEmpty(toEmail))
            {
                Console.WriteLine("Invalid email address.");
                return;
            }

            var toEmails = new JArray { new JObject { { "Email", toEmail } } };
            Console.WriteLine("המייל הוא"+toEmails.Count);
            var request = CreateEmailRequest(toEmails, subject, body);
            await SendMailjetRequest(request);
            Console.WriteLine("SendMailjetRequest");
        }

        private MailjetRequest CreateEmailRequest(JArray toEmails, string subject, string body)
        {
            return new MailjetRequest { Resource = SendV31.Resource }
                .Property(Send.Messages, new JArray
                {
                    new JObject
                    {
                        { "From", new JObject
                            {
                                { "Email", "masterevent792@gmail.com" },
                                { "Name", "master event" }
                            }
                        },
                        { "To", toEmails },
                        { "Subject", subject },
                        { "TextPart", body },
                        { "HTMLPart", $"<h3>{body}</h3>" }
                    }
                });
        }

        private async Task SendMailjetRequest(MailjetRequest request)
        {
            MailjetClient client = new MailjetClient(ApiKey, ApiSecret);
            MailjetResponse response = await client.PostAsync(request);
            Console.WriteLine(response.Content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Email(s) sent successfully!");
            }
            else
            {
                Console.WriteLine($"Failed to send email(s). Status: {response.StatusCode}, Error: {response.GetErrorMessage()}");
            }
        }
    }
}
