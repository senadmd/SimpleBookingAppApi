using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
[Index(nameof(Username))]
[Index(nameof(DateFrom), nameof(DateTo))]
public class Booking
{
    public Booking()
    {
    }
    public Booking(BookingRequest bookingRequest, Room room)
    {
        this.Username = bookingRequest.Username;
        this.Room = room;
        this.DateFrom = bookingRequest.DateFrom;
        this.DateTo = bookingRequest.DateTo;
    }
    public int Id { get; set; } = 0;

    public Room Room { get; set; } = new();
    public string Username { get; set; } = "";

    public DateTime DateFrom { get; set; } = DateTime.MinValue;

    public DateTime DateTo { get; set; } = DateTime.MinValue;
}