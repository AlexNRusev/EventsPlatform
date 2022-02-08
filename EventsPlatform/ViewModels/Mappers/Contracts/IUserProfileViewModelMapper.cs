using EventsPlatform.Dto;

namespace EventsPlatform.ViewModels.Mappers.Contracts
{
    public interface IUserProfileViewModelMapper
    {
        UserProfileViewModel MapFrom(UserProfileDTO entity);
        UserProfileDTO MapFrom(UserProfileViewModel entity);
    }
}
