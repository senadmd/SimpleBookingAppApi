using AutoMapper;

public class BookingBLL
{
    private readonly ILogger<BookingBLL> _logger;
    private readonly IMapper _mapper;
    private readonly BookingRepository _repository;
    public BookingBLL(ILogger<BookingBLL> logger, IMapper mapper, BookingRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    
    public async Task<List<RoomDTO>> GetRoomsAsync()
    {
        var rooms =  await _repository.GetRoomsAsync();
        return _mapper.Map<List<RoomDTO>>(rooms);
    }


    public async Task<List<BookingDTO>> GetBookingsAsync(int roomId, string username)
    {
        var usernameLowercase = username.ToLower().Trim(); //sql-lite is case sensitive by default
        var bookings = await _repository.GetBookingsAsync(roomId, usernameLowercase);
        bookings.ForEach(x=> x.Room = new Room{ Id = roomId}); //for mapping roomId to result
        return _mapper.Map<List<BookingDTO>>(bookings);
    }

    public async Task<bool> RequestOverlapsExistingBookingAsync(BookingRequestDTO bookingRequest)
    {
        return await _repository.ExistsBookingForRoomTimespanAsync(bookingRequest.RoomId, bookingRequest.DateFrom, bookingRequest.DateTo);
    }

    public async Task CreateBookingAsync(BookingRequestDTO bookingRequest)
    {
        var booking = _mapper.Map<Booking>(bookingRequest);
        var room = await _repository.GetRoomAsync(bookingRequest.RoomId);
        if (room == null)
        {
            _logger.LogError($"Booking request contains non-existent roomId, RoomId: {bookingRequest.RoomId}");
            throw new ArgumentException($"Booking request contains non-existent roomId, RoomId: {bookingRequest.RoomId}");
        }
        booking.Room = room;
        await _repository.SaveBookingAsync(booking);
    }


}