using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BookingAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;

    private readonly BookingBLL _bookingBLL;
    public BookingController(ILogger<BookingController> logger, BookingBLL bookingBLL)
    {
        _logger = logger;
        _bookingBLL = bookingBLL;
    }

    [HttpGet()]
    [Route("room/{roomId}/user/{username}")]
    public async Task<List<BookingDTO>> GetBookingsForRoomAndUser(int roomId, string username)
    {
       return await _bookingBLL.GetBookingsAsync(roomId,username);
    }

    [HttpPost()]
    public async Task<ActionResult> CreateBooking([FromBody] BookingRequestDTO bookingRequest)
    {
        if (bookingRequest.DateFrom >= bookingRequest.DateTo)
        {
            _logger.LogError($"Booking request with invalid time interval, DateFrom: {bookingRequest.DateFrom}, DateTo: {bookingRequest.DateTo}");
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        if (await _bookingBLL.RequestOverlapsExistingBookingAsync(bookingRequest)) 
        {
            _logger.LogError($"Booking request overlaps with exising booking, RoomId: {bookingRequest.RoomId}  DateFrom: {bookingRequest.DateFrom}, DateTo: {bookingRequest.DateTo}");
            return StatusCode((int)HttpStatusCode.Conflict);
        }
        await _bookingBLL.CreateBookingAsync(bookingRequest);
        return StatusCode((int)HttpStatusCode.Created);
    }
}
