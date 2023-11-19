using Microsoft.EntityFrameworkCore;

public class BookingDbContext : DbContext
{
    public DbSet<RoomEquipment> RoomEquipment { get; set; }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    public string DbPath { get; }

    public BookingDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, Config.DatabaseFilename);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

}