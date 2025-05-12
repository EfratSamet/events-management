using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;

public class GuestInEventService : IGuestInEventService
{
    private readonly IGuestInEventRepository _repository;
    private readonly ISubGuestRepository _subGuestRepository;
    private readonly ISeatingRepository _seatingRepository;
    private readonly IMapper _mapper;

    public GuestInEventService(IGuestInEventRepository repository, ISubGuestRepository subGuestRepository,  IMapper mapper, ISeatingRepository seatingRepository)
    {
        _repository = repository;
        _subGuestRepository = subGuestRepository;
        _mapper = mapper;
        _seatingRepository = seatingRepository;
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
    public GuestInEventDto GetGuestInEventByGuestId(int guestId)
    {
        return _mapper.Map<GuestInEventDto>(_repository.GetGuestInEventByGuestId(guestId));
    }
    public GuestInEventDto GetGuestInEventByGuestIdAndEventId(int guestId , int eventId)
    {
        return _mapper.Map<GuestInEventDto>(_repository.GetGuestInEventByGuestIdAndEventId(guestId, eventId));
    }


    //סידור אורחים לשולחנות ללא הפרדה

    public async Task<Dictionary<int, List<SubGuestDto>>> AssignSubGuestsToTablesWithoutGenderSeparationAsync(int eventId, int seatsPerTable)
    {
        var guests = _repository.GetGuestsByEventIdOK(eventId); // שולפים רק אורחים שאישרו הגעה
        var subGuestsByGroup = new Dictionary<int, List<SubGuestDto>>(); // מילון לקבוצות

        foreach (var guest in guests)
        {
            var subGuests = (await _subGuestRepository.GetSubGuestsForSeatingAsync(guest.guestId, eventId))
                .Select(sg => new SubGuestDto
                {
                    guestId = sg.guestId,
                    name = sg.name,
                    gender = sg.gender
                }).ToList();

            if (!subGuestsByGroup.ContainsKey(guest.groupId))
            {
                subGuestsByGroup[guest.groupId] = new List<SubGuestDto>();
            }

            subGuestsByGroup[guest.groupId].AddRange(subGuests);
        }

        var sortedGroups = subGuestsByGroup.OrderByDescending(g => g.Value.Count).ToList();

        Dictionary<int, List<SubGuestDto>> tables = new Dictionary<int, List<SubGuestDto>>();
        int tableNumber = 1;
        List<SubGuestDto> currentTable = new List<SubGuestDto>();

        foreach (var group in sortedGroups)
        {
            int index = 0;
            while (index < group.Value.Count)
            {
                int availableSeats = seatsPerTable - currentTable.Count;

                if (availableSeats > 0)
                {
                    currentTable.AddRange(group.Value.Skip(index).Take(availableSeats));
                    index += availableSeats;
                }

                if (currentTable.Count == seatsPerTable)
                {
                    tables[tableNumber] = new List<SubGuestDto>(currentTable);
                    tableNumber++;
                    currentTable.Clear();
                }
            }
        }

        if (currentTable.Any())
        {
            tables[tableNumber] = new List<SubGuestDto>(currentTable);
            // Save remaining seats to the database
            SaveSeatingArrangement(currentTable, eventId, tableNumber);
        }

        return tables;
    }


    //עם הפרדה

    public async Task<Dictionary<int, List<SubGuestDto>>> AssignSubGuestsToTablesWithGenderSeparationAsync(int eventId, int seatsPerTable)
    {
        var guests = _repository.GetGuestsByEventIdOK(eventId);
        var maleSubGuestsByGroup = new Dictionary<int, List<SubGuestDto>>();
        var femaleSubGuestsByGroup = new Dictionary<int, List<SubGuestDto>>();

        foreach (var guest in guests)
        {
            var subGuests = (await _subGuestRepository.GetSubGuestsForSeatingAsync(guest.guestId, eventId))
                           .Select(sg => new SubGuestDto
                           {
                               guestId = sg.guestId,
                               name = sg.name,
                               gender = sg.gender
                           }).ToList();

            foreach (var subGuest in subGuests)
            {
                if (subGuest.gender == Gender.male)
                {
                    if (!maleSubGuestsByGroup.ContainsKey(guest.groupId))
                    {
                        maleSubGuestsByGroup[guest.groupId] = new List<SubGuestDto>();
                    }
                    maleSubGuestsByGroup[guest.groupId].Add(subGuest);
                }
                else
                {
                    if (!femaleSubGuestsByGroup.ContainsKey(guest.groupId))
                    {
                        femaleSubGuestsByGroup[guest.groupId] = new List<SubGuestDto>();
                    }
                    femaleSubGuestsByGroup[guest.groupId].Add(subGuest);
                }
            }
        }

        int tableNumber = 1;
        var maleTables = AssignSubGuestsToTablesByGroup(maleSubGuestsByGroup, seatsPerTable, ref tableNumber);
        var femaleTables = AssignSubGuestsToTablesByGroup(femaleSubGuestsByGroup, seatsPerTable, ref tableNumber);

        return maleTables.Concat(femaleTables).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }


    private Dictionary<int, List<SubGuestDto>> AssignSubGuestsToTablesByGroup(Dictionary<int, List<SubGuestDto>> subGuestsByGroup, int seatsPerTable, ref int tableNumber)
    {
        Dictionary<int, List<SubGuestDto>> tables = new Dictionary<int, List<SubGuestDto>>();
        var sortedGroups = subGuestsByGroup.OrderByDescending(g => g.Value.Count).ToList();
        List<SubGuestDto> currentTable = new List<SubGuestDto>();

        foreach (var group in sortedGroups)
        {
            int index = 0;
            while (index < group.Value.Count)
            {
                int availableSeats = seatsPerTable - currentTable.Count;

                if (availableSeats > 0)
                {
                    currentTable.AddRange(group.Value.Skip(index).Take(availableSeats));
                    index += availableSeats;
                }

                if (currentTable.Count == seatsPerTable)
                {
                    tables[tableNumber] = new List<SubGuestDto>(currentTable);
                    tableNumber++;
                    currentTable.Clear();
                }
            }
        }

        if (currentTable.Any())
        {
            tables[tableNumber] = new List<SubGuestDto>(currentTable);
            tableNumber++;
        }

        return tables;
    }
    private void SaveSeatingArrangement(List<SubGuestDto> subGuests, int eventId, int tableNumber)
    {
        foreach (var subGuest in subGuests)
        {
            // Create a new Seating record
            var seating = new Seating
            {
                eventId = eventId,
                subGuestId = subGuest.guestId,
                table = tableNumber,
                seat = subGuests.IndexOf(subGuest) + 1 // Seat number starts from 1
            };

            // Save the seating arrangement to the database
            _seatingRepository.Add(seating); // Assuming you have an AddSeating method in your repository
        }
    }

}