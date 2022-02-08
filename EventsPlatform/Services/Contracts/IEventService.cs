using EventsPlatform.Dto;
using EventsPlatform.Models;
using EventsPlatform.Models.Enums;

namespace EventsPlatform.Services.Contracts
{
    public interface IEventService
    {
        Task<EventDTO> CreateAsync(EventDTO eventDTO, string userId);
        Task<List<Event>> GetAllByTypeAsync(EventType? eventType, string userId = "");
        Task<EventDTO> GetEventById(int? id);
        Task<List<Event>> GetAllAsync(string userId = "");
        Task UpdateEventAsync(EventDTO model, int eventId, string userId);
        Task<List<EventDTO>> SearchByNameAsync(string name);
        Task<List<EventDTO>> SearchByTownAsync(string town);
    }
}
