using EventsPlatform.Dto;

namespace EventsPlatform.ViewModels.Mappers.Contracts
{
    public interface IEventViewModelMapper
    {
        EventViewModel MapFrom(EventDTO entity);
        EventDTO MapFrom(EventViewModel entity);
    }
}
