using EventsPlatform.Models;

namespace EventsPlatform.Dto.Mappers.Contracts
{
    public interface ILocationDTOMapper
    {
        Location MapFrom(LocationDTO entity);

        LocationDTO MapFrom(Location entity);

    }
}
