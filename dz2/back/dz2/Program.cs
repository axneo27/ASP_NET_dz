using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.FileProviders;
using spr421_spotify_clone.BLL.Services.Genre;
using spr421_spotify_clone.BLL.Services.Auth;
using spr421_spotify_clone.BLL.Services.Email;
using spr421_spotify_clone.BLL.Services.Storage;
using spr421_spotify_clone.BLL.Settings;
using spr421_spotify_clone.BLL.Services.Track;
using spr421_spotify_clone.DAL;
using spr421_spotify_clone.DAL.Entities.Identity;
using spr421_spotify_clone.DAL.Initializer;
using spr421_spotify_clone.DAL.Repositories.Artist;
using spr421_spotify_clone.DAL.Repositories.Genre;
using spr421_spotify_clone.DAL.Repositories.Track;
using spr421_spotify_clone.Infrastructure;
using spr421_spotify_clone.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDb"));
});

// Identity
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

// JWT Auth
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>() ?? new JwtSettings();
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey ?? string.Empty));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero
        };
    });

// Add automapper
builder.Services.AddAutoMapper(options =>
{
    options.LicenseKey = builder.Configuration["Automapper:LicenseKey"];
}, AppDomain.CurrentDomain.GetAssemblies());

// Add repositories
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ITrackRepository, TrackRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

// Add services
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ITrackService, TrackService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("dev", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("dev");

// Request logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Static files
app.AddStaticFiles(app.Environment);

app.MapControllers();

// Seed demo data (roles/users)
await DbSeeder.SeedAsync(app.Services);

// Apply pending migrations (if any)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
