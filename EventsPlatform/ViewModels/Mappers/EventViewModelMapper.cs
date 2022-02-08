using EventsPlatform.Dto;
using EventsPlatform.ViewModels.Mappers.Contracts;

namespace EventsPlatform.ViewModels.Mappers
{
    public class EventViewModelMapper : IEventViewModelMapper
    {
        private readonly ILocationViewModelMapper locationViewModelMapper;

        public EventViewModelMapper(ILocationViewModelMapper locationViewModelMapper)
        {
            this.locationViewModelMapper = locationViewModelMapper ?? throw new ArgumentNullException(nameof(locationViewModelMapper));
        }
        public EventViewModel MapFrom(EventDTO entity)
        {
            return new EventViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                EventStartDate = entity.EventStartDate,
                EventEndDate = entity.EventEndDate,
                MaxNumberOfParticipants = entity.MaxNumberOfParticipants,
                EventType = entity.EventType,
                Details = entity.Details,
                Location = entity.Location != null ? this.locationViewModelMapper.MapFrom(entity.Location) : null
            };
        }

        public EventDTO MapFrom(EventViewModel entity)
        {
            return new EventDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                EventStartDate = entity.EventStartDate,
                EventEndDate = entity.EventEndDate,
                MaxNumberOfParticipants = entity.MaxNumberOfParticipants,
                EventType = entity.EventType,
                Details = entity.Details,
                Location = entity.Location != null ? this.locationViewModelMapper.MapFrom(entity.Location) : null
            };
        }
    }
}
