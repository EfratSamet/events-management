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
    private readonly IGuestRepository _guestRepository;
    private readonly IMapper _mapper;

    public GuestInEventService(IGuestInEventRepository repository, ISubGuestRepository subGuestRepository, IGuestRepository guestRepository, IMapper mapper)
    {
        _repository = repository;
        _subGuestRepository = subGuestRepository;
        _guestRepository = guestRepository;
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




    //סידור אורחים לשולחנות ללא הפרדה

    public async Task<Dictionary<int, List<GuestInEventDto>>> AssignGuestsToTablesWithoutGenderSeparationAsync(int eventId, int seatsPerTable)
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

                var subGuests = await _subGuestRepository.GetSubGuestsForSeatingAsync(guest.guestId, eventId);

                foreach (var subGuest in subGuests)
                {
                    var subGuestDto = new GuestInEventDto
                    {
                        guestId = subGuest.guestId,
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
                    if (!tables.ContainsKey(tableNumber))
                    {
                        tables[tableNumber] = new List<GuestInEventDto>(currentTable);
                        tableNumber++;
                    }
                    currentTable.Clear();
                }
            }
        }

        if (currentTable.Any())
        {
            if (!tables.ContainsKey(tableNumber))
            {
                tables[tableNumber] = new List<GuestInEventDto>(currentTable);
            }
        }

        return tables;
    }
    //עם הפרדה

    public async Task<Dictionary<int, List<GuestInEventDto>>> AssignGuestsToTablesWithGenderSeparationAsync(int eventId, int seatsPerTable)
    {
        var guests = _repository.GetGuestsByEventIdOK(eventId);

        List<GuestInEventDto> maleGuests = new List<GuestInEventDto>();
        List<GuestInEventDto> femaleGuests = new List<GuestInEventDto>();

        Console.WriteLine($"🔍 מתחיל חלוקת אורחים לאירוע {eventId}, מקומות לכל שולחן: {seatsPerTable}");
        Console.WriteLine($"📝 סה\"כ אורחים שנמשכו מהמערכת: {guests.Count}");

        foreach (var guest in guests)
        {
            var guestDto = _mapper.Map<GuestInEventDto>(guest);
            var guestGender = _guestRepository.Get(guest.guestId);

            List<GuestInEventDto> guestWithSubGuests = new List<GuestInEventDto> { guestDto };

            // מושך את תתי האורחים
            var subGuests = await _subGuestRepository.GetSubGuestsForSeatingAsync(guest.guestId, eventId);

            foreach (var subGuest in subGuests)
            {
                var subGuestDto = new GuestInEventDto
                {
                    guestId = subGuest.guestId,
                    eventId = guestDto.eventId,
                    groupId = guestDto.groupId,
                    ok = guestDto.ok
                };
                guestWithSubGuests.Add(subGuestDto);
            }

            // מסווג את כל האורחים (כולל התתי אורחים) לפי מגדר
            foreach (var fullGuest in guestWithSubGuests)
            {
                if (guestGender?.gender == Gender.male)
                {
                    maleGuests.Add(fullGuest);
                    Console.WriteLine($"👨 גבר נוסף: {fullGuest.guestId}");
                }
                else if (guestGender?.gender == Gender.female)
                {
                    femaleGuests.Add(fullGuest);
                    Console.WriteLine($"👩 אישה נוספה: {fullGuest.guestId}");
                }
                else
                {
                    Console.WriteLine($"⚠ אורח {fullGuest.guestId} לא קיבל מגדר ולכן לא נוסף לשום רשימה!");
                }
            }
        }

        Console.WriteLine($"👨‍🦰 גברים: {maleGuests.Count}, 👩 נשים: {femaleGuests.Count}");

        int currentTableNumber = 1; // מתחילים משולחן 1

        // מחלקים את הבנים וממשיכים במספור השולחנות
        var maleTables = AssignGuestsToTablesByGender(maleGuests, seatsPerTable, ref currentTableNumber);

        // מחלקים את הבנות, כשהמספור ממשיך מהנקודה בה נגמרו השולחנות לגברים
        var femaleTables = AssignGuestsToTablesByGender(femaleGuests, seatsPerTable, ref currentTableNumber);

        // מאחדים את טבלאות הגברים והנשים לתוך מילון אחד
        var allTables = maleTables.Concat(femaleTables).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        Console.WriteLine($"📊 סה\"כ שולחנות שהוקצו: {allTables.Count}");
        return allTables;
    }

    private Dictionary<int, List<GuestInEventDto>> AssignGuestsToTablesByGender(List<GuestInEventDto> guests, int seatsPerTable, ref int tableNumber)
    {
        Dictionary<int, List<GuestInEventDto>> tables = new Dictionary<int, List<GuestInEventDto>>();
        List<GuestInEventDto> currentTable = new List<GuestInEventDto>();

        int index = 0;
        while (index < guests.Count)
        {
            int availableSeats = seatsPerTable - currentTable.Count;
            if (availableSeats > 0)
            {
                var guestsToAdd = guests.Skip(index).Take(availableSeats).ToList();
                Console.WriteLine($"🪑 מוסיף {guestsToAdd.Count} אורחים לשולחן {tableNumber}");
                foreach (var guest in guestsToAdd)
                    Console.WriteLine($"➕ אורח {guest.guestId} נוסף לשולחן {tableNumber}");

                currentTable.AddRange(guestsToAdd);
                index += guestsToAdd.Count;
            }

            if (currentTable.Count == seatsPerTable)
            {
                tables[tableNumber] = new List<GuestInEventDto>(currentTable);
                Console.WriteLine($"✅ שולחן {tableNumber} נוצר עם {currentTable.Count} אורחים");
                tableNumber++;
                currentTable.Clear();
            }
        }

        if (currentTable.Any())
        {
            tables[tableNumber] = new List<GuestInEventDto>(currentTable);
            Console.WriteLine($"✅ שולחן {tableNumber} (אחרון) נוצר עם {currentTable.Count} אורחים");
            tableNumber++;
        }

        Console.WriteLine($"📊 סה\"כ שולחנות בקבוצה זו: {tables.Count}");
        return tables;
    }

}