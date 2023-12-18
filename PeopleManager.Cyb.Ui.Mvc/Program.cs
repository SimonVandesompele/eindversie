using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PeopleManagerDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(PeopleManagerDbContext));
    //options.UseSqlServer("Server=.\\SqlExpress;Database=PeopleManager;Trusted_Connection=True;TrustServerCertificate=true");
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<PeopleManagerDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    
    options.LoginPath = "/account/signin";
    options.AccessDeniedPath = "/account/signin";
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<AssignmentService>();
builder.Services.AddScoped<PersonService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using var scope = app.Services.CreateScope();
    
    var dbContext = scope.ServiceProvider.GetRequiredService<PeopleManagerDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }
    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
