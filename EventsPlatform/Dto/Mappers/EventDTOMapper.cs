using EventsPlatform.Dto.Mappers.Contracts;
using EventsPlatform.Models;
using EventsPlatform.Models.Enums;

namespace EventsPlatform.Dto.Mappers
{
    public class EventDTOMapper : IEventDTOMapper
    {
        private readonly ILocationDTOMapper locationDTOMapper;
        public EventDTOMapper(ILocationDTOMapper locationDTOMapper)
        {
            this.locationDTOMapper = locationDTOMapper;
        }

        public Event MapFrom(EventDTO entity)
        {
            return new Event()
            {
                Id = entity.Id,
                Name = entity.Name,
                EventStartDate = entity.EventStartDate,
                EventEndDate = entity.EventEndDate,
                MaxNumberOfParticipants = entity.MaxNumberOfParticipants,
                EventStatus = EventStatus.Active,
                EventType = entity.EventType,
                Details = entity.Details,
                RegisteredParticipants = new List<User>(),
                Organizers = new List<User>(),
                Location = entity.Location != null ? this.locationDTOMapper.MapFrom(entity.Location) : null
            };
        }

        public EventDTO MapFrom(Event entity)
        {
            return new EventDTO()
            {
                Id =entity.Id,
                Name = entity.Name,
                EventStartDate = entity.EventStartDate,
                EventEndDate = entity.EventEndDate,
                MaxNumberOfParticipants = entity.MaxNumberOfParticipants,
                EventType = entity.EventType,
                Details = entity.Details,
                Location = entity.Location != null ? this.locationDTOMapper.MapFrom(entity.Location) : null
            };
        }
    }
}
