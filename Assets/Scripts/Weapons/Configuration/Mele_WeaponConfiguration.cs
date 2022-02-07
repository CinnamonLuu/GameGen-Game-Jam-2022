using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "ScriptableObjects/Melee Weapon")]
public class Mele_WeaponConfiguration : Base_WeaponConfiguration
{
    public override Base_WeaponBehaviour InstantiateWeaponBehaviour() 
    {
        Mele_WeaponBehaviour newBehaviour = new Mele_WeaponBehaviour();
        newBehaviour.SetConfiguration(this);

        return newBehaviour;
    }
}
