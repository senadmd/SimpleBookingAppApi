using System.Linq;
public static class Config
{
    public static readonly string DatabaseFilename = "bookingApp.db";
    public static IEnumerable<RoomEquipment> GetInitialEquipment()
    {
        return ((EquipmentType[])Enum.GetValues(typeof(EquipmentType))).Select(x => new RoomEquipment() { Type = x });
    }
    public static List<Room> GetInitialRooms(IEnumerable<RoomEquipment> equipment)
    {
        return new() {
                new Room(){
                 Equipment = equipment.Where(x=> x.Type == EquipmentType.PHONE || 
                  x.Type == EquipmentType.PROJECTOR).ToList(),
                 Name = "Jolle"},
                new Room(){
                 Equipment = equipment.Where(x=> x.Type == EquipmentType.WHITEBOARD ||
                  x.Type == EquipmentType.PHONE || x.Type == EquipmentType.SCREEN).ToList(),
                 Name = "Nisse"},
                new Room(){
                 Equipment = equipment.Where(x=> x.Type == EquipmentType.PROJECTOR || 
                  x.Type == EquipmentType.PHONE || x.Type == EquipmentType.SCREEN).ToList(),
                 Name = "Rolle"},
                new Room(){
                 Equipment = new(),
                 Name = "Molle"},
        };
    }
}