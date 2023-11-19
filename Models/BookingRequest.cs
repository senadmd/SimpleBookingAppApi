public class BookingRequest
{
    public int RoomId { get; set; } = 0;
    public string Username { get; set; } = "";

    public DateTime DateFrom { get; set; }  = DateTime.MinValue;

    public DateTime DateTo { get; set; } = DateTime.MinValue;
}