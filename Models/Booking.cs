using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
[Index(nameof(Username))]
[Index(nameof(DateFrom), nameof(DateTo))]
public class Booking
{
    public int Id { get; set; } = 0;

    public Room Room { get; set; } = new();
    public string Username { get; set; } = "";

    public DateTime DateFrom { get; set; } = DateTime.MinValue;

    public DateTime DateTo { get; set; } = DateTime.MinValue;
}