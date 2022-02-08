using EventsPlatform.Data;
using EventsPlatform.Dto;
using EventsPlatform.Dto.Mappers.Contracts;
using EventsPlatform.Models;
using EventsPlatform.Models.Enums;
using EventsPlatform.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventsPlatform.Services
{
    public class UserService : IUserService
    {
        private readonly EventsPlatformDbContext dbcontext;
        private readonly IEventDTOMapper eventDTOMapper;
        private readonly IUserProfileDTOMapper userProfileDTOMapper;
        private readonly UserManager<User> userManager;

        public UserService(EventsPlatformDbContext dbcontext,
                            IEventDTOMapper eventDTOMapper,
                            IUserProfileDTOMapper userProfileDTOMapper,
                            UserManager<User> userManager)
        {
            this.dbcontext = dbcontext;
            this.eventDTOMapper = eventDTOMapper;
            this.userProfileDTOMapper = userProfileDTOMapper;
            this.userManager = userManager;
        }

        public async Task<RegisterDTO> CreateAsync(RegisterDTO registerDTO)
        {
            var user = new User()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };
            await userManager.CreateAsync(user, registerDTO.Password);
            await userManager.AddToRoleAsync(user, "User");
            return registerDTO;
        }
        public async Task<SignInStatus> ValidateCredentialAsync(LoginDTO loginDTO)
        {
            var user = await this.dbcontext.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);

            if (user == null) return SignInStatus.Failure;

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
            if (result == PasswordVerificationResult.Failed) return SignInStatus.Failure;
            if (user.IsBanned) return SignInStatus.Banned;

            return SignInStatus.Success;
        }

        public async Task<AppliedEvent> CheckIfEventApplied(string userId, int eventId)
        {
            var user = await this.dbcontext.Users.Include(e => e.AppliedEvents).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new Exception();

            var e = await this.dbcontext.Events.Include(u => u.RegisteredParticipants).FirstOrDefaultAsync(e => e.Id == eventId);
            if (e == null) throw new Exception();

            if (user.AppliedEvents.Any(e => e.Id == eventId)) return AppliedEvent.UnRegistered;

            if (e.RegisteredParticipants.Count >= e.MaxNumberOfParticipants) return AppliedEvent.Full;

            return AppliedEvent.Registered;
        }

        public async Task<AppliedEvent> ApplyEventAsync(string userId, int eventId)
        {
            var user = await this.dbcontext.Users.Include(e => e.AppliedEvents).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new Exception();

            var e = await this.dbcontext.Events.Include(u => u.RegisteredParticipants).FirstOrDefaultAsync(e => e.Id == eventId);
            if (e == null) throw new Exception();

            if(user.AppliedEvents.Any(e => e.Id == eventId))
            {
                user.AppliedEvents.Remove(e);
                await this.dbcontext.SaveChangesAsync();
                return AppliedEvent.UnRegistered;
            }
            
            if(e.RegisteredParticipants.Count >= e.MaxNumberOfParticipants) return AppliedEvent.Full;

            user.AppliedEvents.Add(e);
            await this.dbcontext.SaveChangesAsync();
            return AppliedEvent.Registered;
        }

        public async Task<List<EventDTO>> GetAllEventsAsync(string userId)
        {
            var user = await this.dbcontext.Users.Include(e => e.CreatedEvents).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new Exception();

            var eventDTOs = new List<EventDTO>();
            user.CreatedEvents.ToList().ForEach(e => eventDTOs.Add(this.eventDTOMapper.MapFrom(e)));

            return eventDTOs;
        }

        public async Task<UserProfileDTO> GetUserProfileDataAsync(string userId)
        {
            var user = await this.dbcontext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new Exception();

            var userProfileDTO = this.userProfileDTOMapper.MapFrom(user);
            return userProfileDTO;
        }
    }
}
