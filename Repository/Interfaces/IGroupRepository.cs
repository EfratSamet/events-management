using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    internal interface IGroupRepository : IRepository<Group>
    {
        List<Group> GetGroupsByOrganizerId(string organizerId);
        List<Group> GetGroupsByGuestId(string guestId);
        List<Group> GetGroupsByDateRange(DateTime startDate, DateTime endDate);
        List<Group> GetGroupsByAddress(string address);
        List<Group> GetGroupsByAddressKeyword(string keyword);
    }
}
