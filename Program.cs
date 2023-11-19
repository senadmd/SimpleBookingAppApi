using System.Text.Json.Serialization;
using AutoMapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookingDbContext>();
builder.Services.AddAutoMapper(provider => new MapperConfiguration(cfg =>
     {
         cfg.AddProfile(new BookingProfile());
     }), typeof(Program));
builder.Services.AddTransient(typeof(BookingBLL));
builder.Services.AddTransient(typeof(BookingRepository));

var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    //create database on startup if not exists
    var context = serviceScope?.ServiceProvider.GetRequiredService<BookingDbContext>();
    if (context == null) throw new NotImplementedException($"{nameof(BookingDbContext)} is not initialized!");
    await context.Database.EnsureCreatedAsync();
    if (!context.Rooms.Any())
    {
        //initialize rooms
        context.Rooms.AddRange(Config.GetInitialRooms(Config.GetInitialEquipment()));
        await context.SaveChangesAsync();
    }
}

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
