using Unity.VisualScripting.Dependencies.Sqlite;

public class EquipItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int effectId { get; set; }
    public int value { get; set; }

    [MaxLength (100)]
    public string explain {  get; set; }


}

