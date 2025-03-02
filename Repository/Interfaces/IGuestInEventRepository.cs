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
        Dictionary<string, int> GetOKCountByGroups(string eventId);
        List<GuestInEvent> GuestCountOK(string eventId);
        int CountOK(string eventId);
        int CountOKByGroup(string eventId, string groupName);
    }
}
