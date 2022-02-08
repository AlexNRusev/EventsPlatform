using EventsPlatform.Dto;
using EventsPlatform.Models.Enums;

namespace EventsPlatform.Services.Contracts
{
    public interface IUserService
    {
        Task<RegisterDTO> CreateAsync(RegisterDTO registerDTO);
        Task<SignInStatus> ValidateCredentialAsync(LoginDTO loginDTO);
        Task<AppliedEvent> ApplyEventAsync(string userId, int eventId);
        Task<AppliedEvent> CheckIfEventApplied(string userId, int eventId);
        Task<List<EventDTO>> GetAllEventsAsync(string userId);
        Task<UserProfileDTO> GetUserProfileDataAsync(string userId);
    }
}
