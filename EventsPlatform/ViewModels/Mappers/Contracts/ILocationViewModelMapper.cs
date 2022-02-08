using EventsPlatform.Dto;

namespace EventsPlatform.ViewModels.Mappers.Contracts
{
    public interface ILocationViewModelMapper
    {
        LocationViewModel MapFrom(LocationDTO entity);
        LocationDTO MapFrom(LocationViewModel entity);
    }
}
