using Microsoft.EntityFrameworkCore;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlanner.RazorPages.Pages.Flowers;
using WeddingPlanner.RazorPages.Pages.Users;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerApplication.Services.ServicesImplementation;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerInfrastructure.DB;
using WeddingPlannerInfrastructure.ReposImplementation;
using FlowerService = WeddingPlanner.RazorPages.Pages.Flowers.FlowerService;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddRazorPages();
builder.Services.AddScoped<FlowerService>();
builder.Services.AddScoped<UserServiceR>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<SessionManager>();

builder.Services.AddHttpClient("ApiHttpClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin
              .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader(); // Allow any headers
    });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
