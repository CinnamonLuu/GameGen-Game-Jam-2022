using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_WeaponBehaviour : Base_WeaponBehaviour
{

    Ranged_WeaponConfiguration weaponConfiguration;
    public override void Attack()
    {
        //Instantiate projectiles by pooling
    }

    public override void SetConfiguration(Base_WeaponConfiguration configuration)
    {
        weaponConfiguration = (Ranged_WeaponConfiguration)configuration;
    }

}
