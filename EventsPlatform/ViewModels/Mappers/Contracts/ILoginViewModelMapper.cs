using EventsPlatform.Dto;

namespace EventsPlatform.ViewModels.Mappers.Contracts
{
    public interface ILoginViewModelMapper
    {
        LoginViewModel MapFrom(LoginDTO loginDTO);
        LoginDTO MapFrom(LoginViewModel loginViewModel);
    }
}
