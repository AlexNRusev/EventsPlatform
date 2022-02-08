using EventsPlatform.Models;
using EventsPlatform.Models.Enums;
using EventsPlatform.Services.Contracts;
using EventsPlatform.ViewModels;
using EventsPlatform.ViewModels.Mappers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventsPlatform.Controllers
{
    public class UserController : Controller
    {
        private readonly IRegisterViewModelMapper registerViewModelMapper;
        private readonly ILoginViewModelMapper loginViewModelMapper;
        private readonly IEventViewModelMapper eventViewModelMapper;
        private readonly IUserProfileViewModelMapper userProfileViewModelMapper;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(IRegisterViewModelMapper registerViewModelMapper,
                                ILoginViewModelMapper loginViewModelMapper,
                                IEventViewModelMapper eventViewModelMapper,
                                IUserProfileViewModelMapper userProfileViewModelMapper,
                                IUserService userService,
                                UserManager<User> userManager,
                                SignInManager<User> signInManager)
        {
            this.registerViewModelMapper = registerViewModelMapper;
            this.loginViewModelMapper = loginViewModelMapper;
            this.eventViewModelMapper = eventViewModelMapper;
            this.userProfileViewModelMapper = userProfileViewModelMapper;
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registerDTO = this.registerViewModelMapper.MapFrom(input);
                    await userService.CreateAsync(registerDTO);
                    return RedirectToAction("Login");
                }


                return View(input);
            }
            catch(Exception e)
            {
                return View("Error");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(input);
                }

                var result = await this.userService.ValidateCredentialAsync(loginViewModelMapper.MapFrom(input));

                var a = await signInManager.PasswordSignInAsync(input.UserName, input.Password, true, false);
                var user = await this.userManager.FindByNameAsync(input.UserName);
                var q = signInManager.IsSignedIn(User);

                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index", "Events");
                    case SignInStatus.Banned:
                        return new JsonResult("banned");
                    case SignInStatus.Failure:
                        ModelState.AddModelError("", "Invalid login attempt");
                        return View(input);
                }
                return View(input);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Index", "Events");
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CheckIfEventApplied(ApplyEventViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View("Error");

                var currentUser = await this.userManager.GetUserAsync(User);
                if (currentUser == null) return View("Error");

                var result = await this.userService.CheckIfEventApplied(currentUser.Id, model.EventId);

                if (result == AppliedEvent.Registered)
                    return Json(new { status = "registered" });
                if (result == AppliedEvent.UnRegistered)
                    return Json(new { status = "unregistered" });
                return Json(new { status = "full" });
            }
            catch(Exception e)
            {
                return View("Error");
            }
            
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ApplyEvent(ApplyEventViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View("Error");

                var currentUser = await this.userManager.GetUserAsync(User);
                if (currentUser == null) return View("Error");

                var result = await this.userService.ApplyEventAsync(currentUser.Id, model.EventId);

                if (result == AppliedEvent.Registered)
                    return Json(new { status = "registered" });
                if (result == AppliedEvent.UnRegistered)
                    return Json(new { status = "unregistered" });
                return Json(new { status = "full" });
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var currentUser = await this.userManager.GetUserAsync(User);
                if (currentUser == null) return View("Error");

                var events = new List<EventViewModel>();
                var eventDTOs = await this.userService.GetAllEventsAsync(currentUser.Id);
                if (eventDTOs == null) return View("Error");
                eventDTOs.ForEach(eventDTO => events.Add(this.eventViewModelMapper.MapFrom(eventDTO)));


                return View("Events", events);
            }
            catch(Exception e)
            {
                return View("Error");
            }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var currentUser = await this.userManager.GetUserAsync(User);
                if (currentUser == null) return View("Error");

                var userProfileData = await this.userService.GetUserProfileDataAsync(currentUser.Id);


                return View(this.userProfileViewModelMapper.MapFrom(userProfileData));
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
    }
}
