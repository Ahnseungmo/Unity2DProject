using Unity.VisualScripting.Dependencies.Sqlite;
public enum EquipmentTypes
{
    Helmet = 0,
    Armor = 1,
    Weapon = 2,
    SubWeapon = 3
}
public enum AttackTypes
{
    Mele1H = 0,
    Mele2H = 1,
    MeleDual = 2
}

public class Equipment
{
    public int Characterid {set;get;}
    public EquipmentTypes EquipmentType { set;get;}  
    public AttackTypes AttackType { set;get;}


}
