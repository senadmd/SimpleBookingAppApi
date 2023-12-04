using Microsoft.EntityFrameworkCore;

public class BookingRepository : IDisposable{
    private readonly ILogger<BookingRepository> _logger;
    private readonly BookingDbContext _context;
    public BookingRepository(ILogger<BookingRepository> logger)
    {
        _logger = logger;
        _context = new BookingDbContext();
    }

    public async Task<List<Booking>> GetBookingsAsync(int roomId, string username){
        return await _context.Bookings.Where(x => x.Room.Id == roomId && x.Username == username).Include(x=> x.Room).ToListAsync();
    }

    
    public async Task SaveBookingAsync(Booking booking){
       
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsBookingForRoomTimespanAsync(int roomId, DateTime fromDate, DateTime toDate){
        return await _context.Bookings.AnyAsync(x => x.Room.Id == roomId &&
               x.DateFrom <= toDate && fromDate <= x.DateTo);
    }

    public async Task<List<Room>> GetRoomsAsync(){
        return await _context.Rooms.Include(x=> x.Equipment).ToListAsync();
    }

    public async Task<Room?> GetRoomAsync(int roomId){
        return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}