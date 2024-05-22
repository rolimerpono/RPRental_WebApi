using DatabaseAccess;
using DataServices.Common.DTO;
using DataServices.Services.Implementation;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model;
using RPRENTAL_WEBAPI;
using Serilog;
using System.IO.Pipes;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<IWorker,Worker>();
builder.Services.AddScoped<IApplicationUserService,ApplicationUserService>();
builder.Services.AddScoped<IRoomService,RoomService>();
builder.Services.AddScoped<IRoomNumberService,RoomNumberService>();
builder.Services.AddScoped<IAmenityService,AmenityService>();
builder.Services.AddScoped<IRoomAmenityService,RoomAmenityService>();   
builder.Services.AddScoped<IDBInitializer,DBInitializer>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

string strJwtKey = builder.Configuration.GetValue<string>("JwtSettings:JwtKey")!;


builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    

}).AddJwtBearer(fw =>
{
    fw.RequireHttpsMetadata = false;
    fw.SaveToken = true;
    fw.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(strJwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});



Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    .WriteTo.File("log/rprentallogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();



builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddIdentityCore<ApplicationUser>(option => option.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

builder.Services.AddMvc().AddViewOptions(options => options.HtmlHelperOptions.FormInputRenderMode = FormInputRenderMode.AlwaysUseCurrentCulture);

builder.Services.Configure<IdentityOptions>(opt =>
{

    opt.Password.RequiredLength = 6;
    opt.Password.RequiredUniqueChars = 1;
    opt.Password.RequireUppercase = true;
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer abcdefg@1234567890\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});


builder.Services.AddControllers(options => { options.ReturnHttpNotAcceptable = false;})
    .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

SeedDatabase();

app.MapControllers();

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
        dbInitializer.Initialize();

    }
}
