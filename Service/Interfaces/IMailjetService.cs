using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IMailjetService
    {
        public Task SendEmailAsync(int eventId, string subject, string body);

    }
}
