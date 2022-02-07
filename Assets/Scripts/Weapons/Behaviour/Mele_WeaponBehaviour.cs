using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele_WeaponBehaviour : Base_WeaponBehaviour
{
    Mele_WeaponConfiguration weaponConfiguration;
    LayerMask m_enemiesLayer = 6;
    private float areaTomakeDamage = 2f;

    public override void SetConfiguration(Base_WeaponConfiguration configuration)
    {
        weaponConfiguration = (Mele_WeaponConfiguration)configuration;
    }


    public override void Attack()
    {
        //animacion

        //when animation finish:
        MakeDamage();
    }


    private void MakeDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(m_character.transform.position, areaTomakeDamage);
        Debug.Log(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Enemy enemy = hitColliders[i].gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                float s = Vector3.Dot((m_character.transform.position-enemy.transform.position).normalized,m_character.characterForward);
                if (s > 0)
                {
                    enemy.TakeDamage(weaponConfiguration.GetDamageAmount());
                }
            }
        }
    }
}
