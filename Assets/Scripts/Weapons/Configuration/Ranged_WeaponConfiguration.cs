using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "ScriptableObjects/Ranged Weapon")]
public class Ranged_WeaponConfiguration : Base_WeaponConfiguration
{
    public Stat projectileSpeed;
    public Stat numberOfProjectile;

    public override Base_WeaponBehaviour InstantiateWeaponBehaviour()
    {
        Ranged_WeaponBehaviour newBehaviour = new Ranged_WeaponBehaviour();
        newBehaviour.SetConfiguration(this);
        return newBehaviour;
    }

    public float GetProjectileAmount()
    {
        //add updates
        return numberOfProjectile.amount;
    }
}
