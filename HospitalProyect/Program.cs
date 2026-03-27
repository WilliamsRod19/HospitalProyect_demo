using HospitalProyect.Data;
using HospitalProyect.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// añadidas manualmente
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaulDbConnection"))
);

builder.Services.AddScoped<SpecialtyRepository>();
builder.Services.AddScoped<StaffCategoryRepository>();
builder.Services.AddScoped<StaffRepository>();
builder.Services.AddScoped<AuthRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
