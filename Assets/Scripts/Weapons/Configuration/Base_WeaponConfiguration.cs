using UnityEngine;

public class Base_WeaponConfiguration : ScriptableObject
{
    private Stat damage;
    private Stat attackSpeed;

    public Upgrade[] upgrades;

    public float GetDamageAmount()
    {
        //add updates
        return damage.amount;
    }
    
    public float GetAttackSpeedAmount()
    {
        //add updates
        return attackSpeed.amount;
    }
}
