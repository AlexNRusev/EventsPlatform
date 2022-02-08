using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsPlatform.Data;
using EventsPlatform.Models;
using Microsoft.AspNetCore.Identity;
using EventsPlatform.Services.Contracts;
using EventsPlatform.ViewModels.Mappers.Contracts;
using EventsPlatform.ViewModels;
using EventsPlatform.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace EventsPlatform.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventViewModelMapper eventViewModelMapper;
        private readonly IEventService eventService;
        private readonly UserManager<User> userManager;

        public EventsController(IEventViewModelMapper eventViewModelMapper,
                                IEventService eventService,
                                UserManager<User> userManager)
        {
            this.eventViewModelMapper = eventViewModelMapper;
            this.eventService = eventService;
            this.userManager = userManager;
        }


        // GET: Events
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUser = await this.userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    return View(await this.eventService.GetAllAsync(currentUser.Id));
                }

                var events = await this.eventService.GetAllAsync();
                return View(events);
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Find(EventType? eventType)
        {
            try
            {
                if (eventType == null)
                {
                    return View("Error");
                }
                var currentUser = await this.userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    return View("Index", await this.eventService.GetAllByTypeAsync(eventType, currentUser.Id));
                }

                var events = await this.eventService.GetAllByTypeAsync(eventType);
                if (events == null) return NotFound();
                return View("Index", events);
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        // GET: Events/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var e = await this.eventService.GetEventById(id);
                if (e == null) return NotFound();
                var model = eventViewModelMapper.MapFrom(e);

                return View(model);
            }
            catch(Exception e)
            {
                return View("Error");
            }
            
        }

        // GET: Events/Create
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(EventViewModel input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(input);
                }
                var currentUser = await this.userManager.GetUserAsync(User);
                if (currentUser == null) return NotFound();
                var eventDTO = this.eventViewModelMapper.MapFrom(input);
                if (eventDTO == null) return View("Error");

                await this.eventService.CreateAsync(eventDTO, currentUser.Id);
                return RedirectToAction("Index", "Events");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error";
                return View("Error");
            }
        }

        // GET: Events/Edit/5
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var e = await this.eventService.GetEventById(id);
                if (e == null)
                {
                    return NotFound();
                }
                var model = this.eventViewModelMapper.MapFrom(e);
                return View(model);
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EventViewModel model)
        {

            if(ModelState.IsValid)
            {
                var currentUser = await this.userManager.GetUserAsync(User);
                if (currentUser == null) return View("Error");
                var eventDTO = this.eventViewModelMapper.MapFrom(model);
                if (eventDTO == null) return View("Error");
                await this.eventService.UpdateEventAsync(eventDTO, id, currentUser.Id);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }


        [HttpGet]
        public async Task<List<EventViewModel>> SearchByName(string name)
        {
            var events = new List<EventViewModel>();
            if (ModelState.IsValid)
            {
                var eventDTOs = await this.eventService.SearchByNameAsync(name);
                eventDTOs.ForEach(eventDTO => events.Add(this.eventViewModelMapper.MapFrom(eventDTO)));
            }
            return events;
            
        }
        [HttpGet]
        public async Task<List<EventViewModel>> SearchByTown(string town)
        {
            var events = new List<EventViewModel>();
            if (ModelState.IsValid)
            {
                var eventDTOs = await this.eventService.SearchByTownAsync(town);
                eventDTOs.ForEach(eventDTO => events.Add(this.eventViewModelMapper.MapFrom(eventDTO)));
            }
            return events;
        }


        /*
        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
        */
    }
}
