using EventsPlatform.Dto;
using EventsPlatform.ViewModels.Mappers.Contracts;

namespace EventsPlatform.ViewModels.Mappers
{
    public class LocationViewModelMapper : ILocationViewModelMapper
    {
        public LocationViewModel MapFrom(LocationDTO entity)
        {
            return new LocationViewModel()
            {
                Town = entity.Town,
                Street = entity.Street,
                OtherDetails = entity.OtherDetails,
                EventId = entity.EventId
            };
        }

        public LocationDTO MapFrom(LocationViewModel entity)
        {
            return new LocationDTO()
            {
                Town = entity.Town,
                Street = entity.Street,
                OtherDetails = entity.OtherDetails,
                EventId = entity.EventId
            };
        }
    }
}
