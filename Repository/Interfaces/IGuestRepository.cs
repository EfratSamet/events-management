using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGuestRepository: IRepository<Guest>
    {
        List<Guest> GetGuestsByGroup(int groupId);
        List<Guest?> GetGuestsByEventId(int eventId);
        List<Guest> GetGuestsByOrganizerId(int organizerId);
        List<Guest> GetGuestsByName(string guestName);
        List<Guest> GetGuestsByMail(string mail);
        List<Guest> GetGuestsByGender(Gender gender);
    }
}
//=======
//﻿using Repository.Entity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Repository.Interfaces
//{
//    public interface IGuestRepository: IRepository<Guest>
//    {
//        List<Guest> GetGuestsByGroup(int groupId);
//        List<Guest?> GetGuestsByEventId(int eventId);
//        List<Guest> GetGuestsByOrganizerId(int organizerId);
//        List<Guest> GetGuestsByName(string guestName);
//        List<Guest> GetGuestsByMail(string mail);
//        List<Guest> GetGuestsByGender(Gender gender);
//    }
//}
//>>>>>>> 7ed94bba9db7b9245fce76a26dbbd7fab675585f
