using EventsPlatform.Data;
using EventsPlatform.Dto.Mappers;
using EventsPlatform.Dto.Mappers.Contracts;
using EventsPlatform.Models;
using EventsPlatform.Services;
using EventsPlatform.Services.Contracts;
using EventsPlatform.ViewModels.Mappers;
using EventsPlatform.ViewModels.Mappers.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<EventsPlatformDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(1, 0, 0);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;

})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventsPlatformDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();

// ViewModelMappers
builder.Services.AddScoped<IRegisterViewModelMapper, RegisterViewModelMapper>();
builder.Services.AddScoped<ILoginViewModelMapper, LoginViewModelMapper>();
builder.Services.AddScoped<ILocationViewModelMapper, LocationViewModelMapper>();
builder.Services.AddScoped<IEventViewModelMapper, EventViewModelMapper>();
builder.Services.AddScoped<IUserProfileViewModelMapper, UserProfileViewModelMapper>();


// ServiceMappers
builder.Services.AddScoped<ILocationDTOMapper, LocationDTOMapper>();
builder.Services.AddScoped<IEventDTOMapper, EventDTOMapper>();
builder.Services.AddScoped<IUserProfileDTOMapper, UserProfileDTOMapper>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Events}/{action=Index}/{id?}");

app.Run();
