using EventsPlatform.Dto.Mappers.Contracts;
using EventsPlatform.Models;

namespace EventsPlatform.Dto.Mappers
{
    public class UserProfileDTOMapper : IUserProfileDTOMapper
    {

        public UserProfileDTO MapFrom(User entity)
        {
            return new UserProfileDTO()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Email = entity.Email
            };
        }
    }
}
