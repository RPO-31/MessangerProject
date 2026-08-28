using Mesenger.Api.Services;
using Mesenger.Api.Services.Interfaces;
using Messanger.Api.Services;
using Messanger.Api.Services.Interfaces;
using Messenger.Api.Repository;
using Messenger.Api.Repository.Interfaces;
using Messenger.Api.Repository.Repositories; 
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MesAuth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("https://localhost:5031")
                   .AllowCredentials() // Важно для cookies // фронтенда
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddScoped<ISearchUsersService, SearchUsersService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserRepository, DebugUserRepository>();
builder.Services.AddScoped<IChatRepository, DebugChatRepository>(); 
builder.Services.AddScoped<IChatService, ChatService>();

var app = builder.Build();


app.UseCors("AllowFrontend");
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCookiePolicy();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
