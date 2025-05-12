using AutoMapper;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Repository.Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SeatingService : ISeatingService
    {
        private readonly ISeatingRepository _repository;
        private readonly IMapper _mapper;

        public SeatingService(ISeatingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public SeatingDto Add(SeatingDto item)
        {
            var seating = _mapper.Map<Seating>(item);
            return _mapper.Map<SeatingDto>(_repository.Add(seating));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public SeatingDto Get(int id)
        {
            var seating = _repository.Get(id);
            return _mapper.Map<SeatingDto>(seating);
        }

        public List<SeatingDto> GetAll()
        {
            var seatings = _repository.GetAll();
            return _mapper.Map<List<SeatingDto>>(seatings);
        }

        public SeatingDto Update(int id, SeatingDto item)
        {
            var seating = _mapper.Map<Seating>(item);
            var updatedSeating = _repository.Update(id, seating);
            return _mapper.Map<SeatingDto>(updatedSeating);
        }

        // פונקציות חדשות שמתאימות לפונקציות ברפוזיטורי

        // מחזיר את כל מזהי האורחים לפי מזהה אורח
        public List<int> GetSubGuestsIdsByGuestId(int guestId)
        {
            return _repository.GetSubGuestsIdsByGuestId(guestId);
                            
        }

        // מחזיר את כל מזהי האורחים לפי מספר שולחן
        public List<int> GetSubGuestsIdsByTable(int tableNumber)
        {
            return _repository.GetSubGuestsIdsByTable(tableNumber);
                               
        }

        // מחזיר את מספר השולחן לפי מזהה אורח
        public int? GetTableByGuestId(int guestId)
        {
            return _repository.GetTableByGuestId(guestId);
        }
        public void AssignSeats(List<SeatingDto> seatings)
        {
            var seating = _mapper.Map<List<Seating>>(seatings);
            _repository.AssignSeats(seating);
        }
    }
}
