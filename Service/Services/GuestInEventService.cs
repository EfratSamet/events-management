using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GuestInEventService : IService<GuestInEventDto>
    {
        private readonly IGuestInEventRepository _repository;
        private readonly IMapper _mapper;

        public GuestInEventService(IGuestInEventRepository repository, IMapper mapper)
        {
            _repository = repository;
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

        public GuestInEventDto Update(int id, GuestInEventDto item)
        {
            return _mapper.Map<GuestInEventDto>(_repository.Update(id, _mapper.Map<GuestInEvent>(item)));
        }
             // פונקציה שמקבלת את מספר האירוע ומספר המושבים בשולחן ומחזירה מיפוי של אורחים לשולחנות
        public Dictionary<int, List<GuestInEventDto>> AssignGuestsToTables(int eventId, int seatsPerTable)
        {
            // שליפת האורחים לפי אישורי הגעה ולפי הקטגוריה
            var guestsByGroup = _repository.GuestCountOK(eventId);

            Dictionary<int, List<GuestInEventDto>> tables = new Dictionary<int, List<GuestInEventDto>>();
            int tableNumber = 1;
            List<GuestInEventDto> currentTable = new List<GuestInEventDto>();

            // סידור האורחים בשולחנות
            foreach (var group in guestsByGroup)
            {
                var groupGuests = _repository.GetAll()
                    .Where(g => g.eventId == eventId && g.ok == true ) 
                    .ToList();

                foreach (var guest in groupGuests)
                {
                    if (currentTable.Count < seatsPerTable)
                    {
                        currentTable.Add(_mapper.Map<GuestInEventDto>(guest));
                    }
                    else
                    {
                        tables[tableNumber] = new List<GuestInEventDto>(currentTable);
                        tableNumber++;
                        currentTable.Clear();
                        currentTable.Add(_mapper.Map<GuestInEventDto>(guest));
                    }
                }
            }

            // אם נשארו אורחים בשולחן אחרון שלא הוזנו
            if (currentTable.Any())
            {
                tables[tableNumber] = currentTable;
            }

            return tables;
        }
    }
}
