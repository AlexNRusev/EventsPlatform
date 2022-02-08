using EventsPlatform.Data;
using EventsPlatform.Dto;
using EventsPlatform.Dto.Mappers.Contracts;
using EventsPlatform.Models;
using EventsPlatform.Models.Enums;
using EventsPlatform.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EventsPlatform.Services
{
    public class EventService : IEventService
    {
        private readonly EventsPlatformDbContext dbcontext;
        private readonly IEventDTOMapper eventDTOMapper;

        public EventService(EventsPlatformDbContext dbcontext,
                            IEventDTOMapper eventDTOMapper)
        {
            this.dbcontext = dbcontext;
            this.eventDTOMapper = eventDTOMapper;
        }

        public async Task<EventDTO> CreateAsync(EventDTO eventDTO, string userId)
        {
            var e = this.eventDTOMapper.MapFrom(eventDTO);
            if (e == null) throw new ArgumentNullException();
            var user = await this.dbcontext.Users.Include(e => e.CreatedEvents).Include(e => e.AppliedEvents).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new Exception();

            user.CreatedEvents.Add(e);



            await this.dbcontext.Events.AddAsync(e);
            await this.dbcontext.SaveChangesAsync();

            return eventDTO;
        }

        public async Task<List<Event>> GetAllByTypeAsync(EventType? eventType, string userId = "")
        {
            if (eventType == null) throw new ArgumentNullException();
            var events = await this.dbcontext.Events.Include(u => u.Location).Include(u => u.RegisteredParticipants).Include(u => u.Organizers).Where(e => e.EventType == eventType).ToListAsync();
            if (events == null) throw new Exception();
            if (userId != "")
            {
                events = events.Where(e => e.Organizers.Any(u => u.Id != userId)).ToList();
            }
            return events;
        }

        public async Task<EventDTO> GetEventById(int? id)
        {
            if (id == null) throw new ArgumentNullException();
            var e = await this.dbcontext.Events.Include(u => u.Location).FirstOrDefaultAsync(u => u.Id == id);
            if (e == null) throw new Exception();
            var model = eventDTOMapper.MapFrom(e);
            return model;
        }

        public async Task<List<Event>> GetAllAsync(string userId = "")
        {
            var events = await this.dbcontext.Events.Include(u => u.Location).Include(u => u.RegisteredParticipants).Include(u => u.Organizers).ToListAsync();
            if (events == null) throw new Exception();
            if (userId != "")
            {
                events = events.Where(e => e.Organizers.Any(u => u.Id != userId)).ToList();
            }
            
            return events;

        }

        public async Task UpdateEventAsync(EventDTO model, int eventId, string userId)
        {
            var e = await this.dbcontext.Events.Include(u => u.Location).Include(u => u.Organizers).FirstOrDefaultAsync(u => u.Id == eventId);
            if (e == null) throw new Exception();

            if (!e.Organizers.Any(user => user.Id == userId)) throw new UnauthorizedAccessException();
            e.Name = model.Name;
            e.EventStartDate = model.EventStartDate;
            e.EventEndDate = model.EventEndDate;
            e.MaxNumberOfParticipants = model.MaxNumberOfParticipants;
            e.Details = model.Details;
            if(model.Location != null && e.Location != null)
            {
                e.Location.Town = model.Location.Town;
                e.Location.Street = model.Location.Street;
                e.Location.OtherDetails = model.Location.OtherDetails;
            }
            

            this.dbcontext.Update(e);
            await this.dbcontext.SaveChangesAsync();
        }

        public async Task<List<EventDTO>> SearchByNameAsync(string name)
        {
            var events = new List<EventDTO>();

            var allEvents = await this.dbcontext.Events.Include(e => e.Location).ToListAsync();
            if (allEvents == null) return events;

            for (int i = 0; i < allEvents.Count; i++)
            {
                var currEvent = allEvents[i];
                if (currEvent.Name.ToLower().Contains(name.ToLower())) events.Add(this.eventDTOMapper.MapFrom(currEvent));
            }

            return events;

        }

        public async Task<List<EventDTO>> SearchByTownAsync(string town)
        {

            var events = new List<EventDTO>();

            var allEvents = await this.dbcontext.Events.Include(e => e.Location).ToListAsync();
            if (allEvents == null) return events;

            for (int i = 0; i < allEvents.Count; i++)
            {
                var currEvent = allEvents[i];
                if (currEvent.Location != null && currEvent.Location.Town != null && currEvent.Location.Town.ToLower().Equals(town.ToLower())) events.Add(this.eventDTOMapper.MapFrom(currEvent));
            }

            return events;

        }
    }
}
