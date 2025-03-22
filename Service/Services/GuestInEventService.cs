using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;

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
        return _mapper.Map<List<GuestInEventDto>>(_repository.GetGuestsByEventIdOK(eventId));
    }
    public GuestInEventDto Update(int id, GuestInEventDto item)
    {
        return _mapper.Map<GuestInEventDto>(_repository.Update(id, _mapper.Map<GuestInEvent>(item)));
    }




    //סידור אורחים לשולחנות

    public async Task<Dictionary<int, List<GuestInEventDto>>> AssignGuestsToTablesWithSubGuestsAsync(int eventId, int seatsPerTable)
    {
        var guests = _repository.GetGuestsByEventIdOK(eventId);
        var guestsByGroup = guests
            .GroupBy(g => g.groupId)
            .OrderByDescending(g => g.Count())  // מיון קבוצות מהגדולה לקטנה
            .ToList();

        Dictionary<int, List<GuestInEventDto>> tables = new Dictionary<int, List<GuestInEventDto>>();
        int tableNumber = 1;
        List<GuestInEventDto> currentTable = new List<GuestInEventDto>();

        foreach (var group in guestsByGroup)
        {
            List<GuestInEventDto> groupGuestsWithSubGuests = new List<GuestInEventDto>();

            foreach (var guest in group)
            {
                var guestDto = _mapper.Map<GuestInEventDto>(guest);
                groupGuestsWithSubGuests.Add(guestDto);

                // מחכים לתתי האורחים בצורה אסינכרונית
                var subGuests = await _subGuestRepository.GetSubGuestsForSeatingAsync(guest.guestId, eventId);

                foreach (var subGuest in subGuests)
                {
                    var subGuestDto = new GuestInEventDto
                    {
                        guestId = subGuest.guestId, // מזהה שונה
                        eventId = guestDto.eventId,
                        groupId = guestDto.groupId,
                        ok = guestDto.ok
                    };

                    groupGuestsWithSubGuests.Add(subGuestDto);
                }
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