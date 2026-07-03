using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Repos;
using RestaurantManagement.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RmDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("RmDbContext")));

builder.Services.AddScoped<KitchenTaskRepo>();
builder.Services.AddScoped<UserInfoRepo>();
builder.Services.AddScoped<CurrentUserHelper>();
builder.Services.AddScoped<InventoryRepo>();
builder.Services.AddScoped<CategoriesRepo>();
builder.Services.AddHttpContextAccessor();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
