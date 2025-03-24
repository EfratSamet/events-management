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
        private const string ApiKey = "8efb34610befbe31c810ddc0d25d939c";
        private const string ApiSecret = "7270a0984c8ac854aedd3157428523c9";

        private readonly IGuestService _guestService;

        public MailjetService(IGuestService guestService)
        {
            _guestService = guestService;
        }

        public async Task SendEmailAsync(int eventId, string subject, string body)
        {
            var guests = _guestService.GetGuestsByEventId(eventId);
            if (guests == null || guests.Count == 0)
            {
                Console.WriteLine("No guests found for the event.");
                return;
            }

            var toEmails = new JArray();
            foreach (var guest in guests)
            {
                if (!string.IsNullOrEmpty(guest?.mail))
                {
                    toEmails.Add(new JObject { { "Email", guest.mail } });
                }
            }

            if (toEmails.Count == 0)
            {
                Console.WriteLine("No valid email addresses found.");
                return;
            }

            var request = CreateEmailRequest(toEmails, subject, body);
            await SendMailjetRequest(request);
        }

        public async Task SendSingleEmailAsync(string toEmail, string subject, string body)
        {
            if (string.IsNullOrEmpty(toEmail))
            {
                Console.WriteLine("Invalid email address.");
                return;
            }

            var toEmails = new JArray { new JObject { { "Email", toEmail } } };
            var request = CreateEmailRequest(toEmails, subject, body);
            await SendMailjetRequest(request);
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
                                { "Email", "sametfamily21@gmail.com" },
                                { "Name", "Efrat Samet" }
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
