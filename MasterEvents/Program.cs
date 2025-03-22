using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using System.Text;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;
using Repository.Entity;
using Repository.Repositories;
using AutoMapper;
using Mock;

var builder = WebApplication.CreateBuilder(args);

// טעינת appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// בדיקה שהערכים של JWT לא ריקים
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer is missing in configuration.");
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience is missing in configuration.");
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing in configuration.");

// הוספת שירותי אימות עם JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddScoped<IMailjetService, MailjetService>();

builder.Services.AddScoped<ILoginService, LoginService>();


builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGroupService, GroupService>();

builder.Services.AddScoped<IRepository<Guest>, GuestRepository>();
builder.Services.AddScoped<IService<GuestDto>, GuestService>();

builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IGuestService, GuestService>();

//builder.Services.AddScoped<IRepository<GuestInEvent>, GuestInEventRepository>();
builder.Services.AddScoped<IGuestInEventService, GuestInEventService>();
builder.Services.AddScoped<IGuestInEventRepository, GuestInEventRepository>();

builder.Services.AddScoped<IOrganizerService, OrganizerService>();
builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IRepository<PhotosFromEvent>, PhotosFromEventRepository>();
builder.Services.AddScoped<IService<PhotosFromEventDto>, PhotosFromEventService>();

builder.Services.AddScoped<IRepository<Seating>, SeatingRepository>();
builder.Services.AddScoped<IService<SeatingDto>, SeatingService>();

builder.Services.AddScoped<ISubGuestRepository, SubGuestRepository>();
builder.Services.AddScoped<ISubGuestService, SubGuestService>();

builder.Services.AddScoped<IContext, MyDataBase>();
builder.Services.AddAutoMapper(typeof(MyMapper));

// הוספת שירותי Authorization
builder.Services.AddAuthorization();

// הוספת תמיכה ב-Controllers
builder.Services.AddControllers();

// הוספת Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


// הגדרת CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});

var app = builder.Build();

// הפעלת Swagger (בפיתוח בלבד)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// הפעלת אימות והרשאות
app.UseAuthentication();
app.UseAuthorization();

// הפעלת CORS
app.UseCors(MyAllowSpecificOrigins);

// מיפוי כל ה-Controllers
app.MapControllers();

app.Run();
