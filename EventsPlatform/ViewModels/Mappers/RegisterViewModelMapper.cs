using EventsPlatform.Dto;
using EventsPlatform.ViewModels.Mappers.Contracts;

namespace EventsPlatform.ViewModels.Mappers
{
    public class RegisterViewModelMapper : IRegisterViewModelMapper
    {
        public RegisterViewModel MapFrom(RegisterDTO entity)
        {
            return new RegisterViewModel()
            {
                UserName = entity.UserName,
                Password = entity.Password,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }

        public RegisterDTO MapFrom(RegisterViewModel entity)
        {
            return new RegisterDTO()
            {
                UserName = entity.UserName,
                Password = entity.Password,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }
}
