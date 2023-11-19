using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Room {
    public int Id { get; set; } = 0;
    public List<RoomEquipment> Equipment { get; set; } = new();
    public string Name { get; set; } = "";

}