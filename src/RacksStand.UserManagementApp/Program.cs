using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Contexts;
using RacksStand.Persistence.Repositories;
using RacksStand.UnifiedServices.DataServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<RacksStandContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<RacksStandContext>());
builder.Services.AddScoped<IRacksStandContext>(sp => sp.GetRequiredService<RacksStandContext>());
builder.Services.AddScoped<IdentityContext>(sp => sp.GetRequiredService<RacksStandContext>());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPlanService, PlanService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
