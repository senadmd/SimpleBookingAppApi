using AutoMapper;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking,BookingDTO>();
        CreateMap<Room,RoomDTO>();
        CreateMap<RoomEquipment, RoomEquipmentDTO>();
        CreateMap<BookingRequestDTO,Booking>();
    }
}