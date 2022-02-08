using EventsPlatform.Models;

namespace EventsPlatform.Dto.Mappers.Contracts
{
    public interface IUserProfileDTOMapper
    {
        UserProfileDTO MapFrom(User entity);
    }
}
