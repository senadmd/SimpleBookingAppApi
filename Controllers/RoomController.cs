using Microsoft.AspNetCore.Mvc;

namespace BookingAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;

    private readonly BookingBLL _bookingBLL;
    public RoomController(ILogger<RoomController> logger, BookingBLL bookingBLL)
    {
        _logger = logger;
        _bookingBLL = bookingBLL;
    }

    [HttpGet()]
    public async Task<List<RoomDTO>> GetRooms()
    {
       return await _bookingBLL.GetRoomsAsync();
    }

}