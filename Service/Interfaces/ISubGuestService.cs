using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISubGuestService : IService<SubGuestDto>
    {
        List<SeatingDto> ArrangeSeating(int eventId);
    }
}
