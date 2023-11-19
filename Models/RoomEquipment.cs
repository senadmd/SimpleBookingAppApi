using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class RoomEquipment
{
    public int Id { get; set; } = 0;
    
    public EquipmentType Type { get; set; } = EquipmentType.OTHER;

    public Room? Room { get; } = null;

}