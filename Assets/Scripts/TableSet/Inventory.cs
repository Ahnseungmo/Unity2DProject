using Unity.VisualScripting.Dependencies.Sqlite;


public class Inventory
{
    [PrimaryKey,AutoIncrement]
    public int InventoryID {  get; set; }
    public int CharacterID { get; set; }
    public ItemTypes ItemType { get; set; }
    public int SlotIndex { get; set; }

    public int ItemId {  get; set; }


}
