using DatabaseAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Model;
using RPRENTAL_WEBAPP;
using RPRENTAL_WEBAPP.Services.Implementation;
using RPRENTAL_WEBAPP.Services.Interface;
using System.ComponentModel.Design;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IBaseService,BaseService>();
builder.Services.AddScoped<IHelperService, HelperService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IApplicationUserService,ApplicationUserService>();


builder.Services.AddHttpClient<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomService,RoomService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(100);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => 
{
    options.Cookie.HttpOnly = true;    
    options.SlidingExpiration = true;
    options.LoginPath = "/ApplicationUser/Login/";
    options.AccessDeniedPath = "/ApplicationUser/AccessDenied/";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(100);
});

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequiredUniqueChars = 1;
    opt.Password.RequireUppercase = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
