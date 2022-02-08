using EventsPlatform.Dto.Mappers.Contracts;
using EventsPlatform.Models;

namespace EventsPlatform.Dto.Mappers
{
    public class LocationDTOMapper : ILocationDTOMapper
    {
        public Location MapFrom(LocationDTO entity)
        {
            return new Location()
            {
                Town = entity.Town,
                Street = entity.Street,
                OtherDetails = entity.OtherDetails,
                EventId = entity.EventId
            };
        }

        public LocationDTO MapFrom(Location entity)
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
