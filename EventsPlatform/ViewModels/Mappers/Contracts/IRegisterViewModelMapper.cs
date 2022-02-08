using EventsPlatform.Dto;

namespace EventsPlatform.ViewModels.Mappers.Contracts
{
    public interface IRegisterViewModelMapper
    {
        RegisterViewModel MapFrom(RegisterDTO entity);
        RegisterDTO MapFrom(RegisterViewModel entity);
    }
}
