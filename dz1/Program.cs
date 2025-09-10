using Microsoft.EntityFrameworkCore;
using dz1;
using dz1.Initializer;
using dz1.Repositories.Categories;
using dz1.Repositories.Products;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("LocalDb");
    options.UseSqlServer(connectionString);
});

// Add repositories

// Прописує в Dependency injection по патерну Singleton
// Об'єкт класу буде існувати в одному екземплярі
//builder.Services.AddSingleton<CategoryRepository>();

// Об'єкт буде створюватися при кожному використанні
//builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

// Об'єкт буде створюватися для кожного HTTP запиту
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Add session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Seed();

app.Run();