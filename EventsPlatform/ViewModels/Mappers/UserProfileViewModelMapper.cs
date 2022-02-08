using EventsPlatform.Dto;
using EventsPlatform.ViewModels.Mappers.Contracts;

namespace EventsPlatform.ViewModels.Mappers
{
    public class UserProfileViewModelMapper : IUserProfileViewModelMapper
    {
        public UserProfileViewModel MapFrom(UserProfileDTO entity)
        {
            return new UserProfileViewModel()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Email = entity.Email
            };
        }

        public UserProfileDTO MapFrom(UserProfileViewModel entity)
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
