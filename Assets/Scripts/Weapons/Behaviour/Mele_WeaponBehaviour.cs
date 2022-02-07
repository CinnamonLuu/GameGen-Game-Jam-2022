using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele_WeaponBehaviour : Base_WeaponBehaviour
{
    Mele_WeaponConfiguration weaponConfiguration;
    LayerMask m_enemiesLayer = 6;
    private float areaTomakeDamage = 4f;
    public override void Attack()
    {
        //animacion

        //when animation finish:
        MakeDamage();
    }


    private void MakeDamage()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + gameObject.transform.forward * (areaTomakeDamage / 2f), new Vector3(areaTomakeDamage / 2f, 1, areaTomakeDamage / 2f), Quaternion.identity, m_enemiesLayer);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Enemy enemy = hitColliders[i].gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(weaponConfiguration.GetDamageAmount());
        }
    }
}
