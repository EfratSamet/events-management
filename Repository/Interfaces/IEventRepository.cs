using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        List<Event> GetEventsByOrganizerId(int organizerId);
    }

}
