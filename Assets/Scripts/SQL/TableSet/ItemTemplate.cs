using Unity.VisualScripting.Dependencies.Sqlite;
public enum ItemTypes
{
    Equip = 0,
    Consumable = 1,
    Material = 2,
}
public class ItemTemplate
{
    [PrimaryKey, AutoIncrement]
    public int ItemTemplateID { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    public ItemTypes ItmeType { get; set; }
    public bool IsStack { get; set; } = true;
    public string Description { get; set; }

}
