using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;

    public BookingController(ILogger<BookingController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    [Route("room/{roomId}/user/{username}")]
    public async Task<List<Booking>> GetBookingsForRoomAndUser(int roomId, string username)
    {
        using var db = new BookingDbContext();
        var usernameLowercase = username.ToLower().Trim();
        return await db.Bookings.Where(x => x.Room.Id == roomId && x.Username == usernameLowercase)
               .Include(x => x.Room).ThenInclude(x => x.Equipment).ToListAsync();
    }

    [HttpPost()]
    public async Task<ActionResult> CreateBooking([FromBody] BookingRequest bookingRequest)
    {
        using var db = new BookingDbContext();
        if (bookingRequest.DateFrom >= bookingRequest.DateTo)
        {
            _logger.LogError($"Booking request with invalid time interval, DateFrom: {bookingRequest.DateFrom}, DateTo: {bookingRequest.DateTo}");
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        var roomToBook = await db.Rooms.FirstOrDefaultAsync(x => x.Id == bookingRequest.RoomId);
        if (roomToBook == null)
        {
            _logger.LogError($"Booking request contains non-existent roomId, RoomId: {bookingRequest.RoomId}");
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        if (await db.Bookings.AnyAsync(x =>x.Room.Id == bookingRequest.RoomId && x.DateFrom <= bookingRequest.DateTo && bookingRequest.DateFrom <= x.DateTo)) 
        {
            _logger.LogError($"Booking request overlaps with exising booking, RoomId: {bookingRequest.RoomId}  DateFrom: {bookingRequest.DateFrom}, DateTo: {bookingRequest.DateTo}");
            return StatusCode((int)HttpStatusCode.Conflict);
        }
        var bookingToCreate = new Booking(bookingRequest, roomToBook);
        db.Bookings.Add(bookingToCreate);
        await db.SaveChangesAsync();
        return StatusCode((int)HttpStatusCode.Created);
    }
}
