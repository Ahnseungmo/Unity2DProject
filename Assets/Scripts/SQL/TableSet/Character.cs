
using Unity.VisualScripting.Dependencies.Sqlite;

public class Character
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [MaxLength(100)]
    public string name { get; set; }
    public int Hp { get; set; } = 100;

    public int[] WeaponIds;
    public int[] EquipmentIds;

}