using AutoMapper;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking,BookingDTO>().ForCtorParam("RoomId", opt => opt.MapFrom(s => s.Room.Id));;
        CreateMap<Room,RoomDTO>();
        CreateMap<RoomEquipment, RoomEquipmentDTO>();
        CreateMap<BookingRequestDTO,Booking>();
    }
}