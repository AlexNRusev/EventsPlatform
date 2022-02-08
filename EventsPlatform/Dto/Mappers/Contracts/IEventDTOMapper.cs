using EventsPlatform.Models;

namespace EventsPlatform.Dto.Mappers.Contracts
{
    public interface IEventDTOMapper
    {
        Event MapFrom(EventDTO eventDTO);
        EventDTO MapFrom(Event e);
    }
}
