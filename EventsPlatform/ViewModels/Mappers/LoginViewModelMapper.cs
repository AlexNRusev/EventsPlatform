using EventsPlatform.Dto;
using EventsPlatform.ViewModels.Mappers.Contracts;

namespace EventsPlatform.ViewModels.Mappers
{
    public class LoginViewModelMapper : ILoginViewModelMapper
    {
        public LoginViewModel MapFrom(LoginDTO entity)
        {
            return new LoginViewModel()
            {
                UserName = entity.UserName,
                Password = entity.Password
            };
        }

        public LoginDTO MapFrom(LoginViewModel entity)
        {
            return new LoginDTO()
            {
                UserName = entity.UserName,
                Password = entity.Password
            };
        }
    }
}
