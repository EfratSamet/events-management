using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class GuestInEventService : IGuestInEventService
    {
        private readonly IGuestInEventRepository _repository;
        private readonly ISubGuestRepository _subGuestRepository;
        private readonly IMapper _mapper;

        public GuestInEventService(IGuestInEventRepository repository, ISubGuestRepository subGuestRepository, IMapper mapper)
        {
            _repository = repository;
            _subGuestRepository = subGuestRepository;
            _mapper = mapper;
        }

        public GuestInEventDto Add(GuestInEventDto item)
        {
            return _mapper.Map<GuestInEventDto>(_repository.Add(_mapper.Map<GuestInEvent>(item)));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public GuestInEventDto Get(int id)
        {
            return _mapper.Map<GuestInEventDto>(_repository.Get(id));
        }

        public List<GuestInEventDto> GetAll()
        {
            return _mapper.Map<List<GuestInEventDto>>(_repository.GetAll());
        }

        public List<GuestInEventDto> GetGuestsByEventId(int eventId)
        {
            return _mapper.Map<List<GuestInEventDto>>(_repository.GetGuestsByEventId(eventId));
        }

        public List<GuestInEventDto> GetGuestsByEventIdOk(int eventId)
        {
            return _mapper.Map<List<GuestInEventDto>>(_repository.GetGuestsByEventIdOk(eventId));
        }
        public GuestInEventDto Update(int id, GuestInEventDto item)
        {
            return _mapper.Map<GuestInEventDto>(_repository.Update(id, _mapper.Map<GuestInEvent>(item)));
        }




        //סידור אורחים לשולחנות

        public async Task<Dictionary<int, List<GuestInEventDto>>> AssignGuestsToTablesWithSubGuestsAsync(int eventId, int seatsPerTable)
        {

            // שליפת האורחים לפי אישורי הגעה ולפי הקטגוריה
            var guestsByGroup = _repository.GuestCountOK(eventId);

            Dictionary<int, List<GuestInEventDto>> tables = new Dictionary<int, List<GuestInEventDto>>();
            int tableNumber = 1;
            List<GuestInEventDto> currentTable = new List<GuestInEventDto>();

            foreach (var group in guestsByGroup)
            {

                var groupGuests = _repository.GetAll()
                    .Where(g => g.eventId == eventId && g.ok == true ) 
                    .ToList();

                foreach (var guest in group)
                {
                    var guestDto = _mapper.Map<GuestInEventDto>(guest);
                    groupGuestsWithSubGuests.Add(guestDto);

                    // מחכים לתתי האורחים בצורה אסינכרונית
                    var subGuests = await _subGuestRepository.GetSubGuestsForSeatingAsync(guest.guestId, eventId);
                    var subGuestDtos = subGuests.Select(sg => _mapper.Map<GuestInEventDto>(sg)).ToList();
                    groupGuestsWithSubGuests.AddRange(subGuestDtos);
                }

                int index = 0;
                while (index < groupGuestsWithSubGuests.Count)
                {
                    int availableSeats = seatsPerTable - currentTable.Count;

                    if (availableSeats > 0)
                    {
                        currentTable.AddRange(groupGuestsWithSubGuests.Skip(index).Take(availableSeats));
                        index += availableSeats;
                    }

                    if (currentTable.Count == seatsPerTable)
                    {
                        tables[tableNumber] = new List<GuestInEventDto>(currentTable);
                        tableNumber++;
                        currentTable.Clear();
                    }
                }
            }

            // הוספת שולחן אחרון אם נשארו אורחים
            if (currentTable.Any())
            {
                tables[tableNumber] = currentTable;
            }

            return tables;
        }


    }
}
