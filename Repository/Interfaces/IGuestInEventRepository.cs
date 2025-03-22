using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGuestInEventRepository : IRepository<GuestInEvent>
    {
        List<GuestInEvent> GuestCountOK(int eventId);
        int CountOK(int eventId);
    }
}
