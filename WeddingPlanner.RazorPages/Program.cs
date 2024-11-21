using Microsoft.EntityFrameworkCore;
using WeddingPlanner.RazorPages.Pages.Flowers;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerApplication.Services.ServicesImplementation;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerInfrastructure.DB;
using WeddingPlannerInfrastructure.ReposImplementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFlowerService, FlowerService>();
builder.Services.AddScoped<IFlowerRepo, FlowerRepo>();
// Add services to the container.
builder.Services.AddRazorPages();



var app = builder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register other services
builder.Services.AddScoped<IFlowerRepo, FlowerRepo>();
builder.Services.AddScoped<IFlowerService, FlowerService>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
