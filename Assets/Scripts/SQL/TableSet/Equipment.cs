using Unity.VisualScripting.Dependencies.Sqlite;
public enum DBEquipmentTypes
{
    Helmet = 0,
    Armor = 1,
    Weapon = 2,
    SubWeapon = 3
}
public enum DBAttackTypes
{
    Mele1H = 0,
    Mele2H = 1,
    MeleDual = 2
}

public class DBEquipment
{
    public int Characterid {set;get;}
    public DBEquipmentTypes EquipmentType { set;get;}  
    public DBAttackTypes AttackType { set;get;}


}
