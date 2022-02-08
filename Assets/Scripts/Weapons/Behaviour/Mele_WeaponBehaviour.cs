using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele_WeaponBehaviour : Base_WeaponBehaviour
{
    Mele_WeaponConfiguration weaponConfiguration;
    LayerMask m_enemiesLayer = 6;
    private float areaTomakeDamage = 8f;

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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(m_characterCombatConfiguration.transform.position, areaTomakeDamage, 1<< m_characterCombatConfiguration.target);
        Debug.Log(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Enemy enemy = hitColliders[i].gameObject.GetComponent<Enemy>();
            CombatConfiguration target = hitColliders[i].gameObject.GetComponent<CombatConfiguration>();
            /*if (enemy != null)
            {
                Vector3 vectorToEnemy = enemy.transform.position - m_character.transform.position;
                float s = Vector2.Dot(new Vector2(vectorToEnemy.x, vectorToEnemy.y), new Vector2(m_character.characterForward.x, m_character.characterForward.y));
                if (s > 0)
                {
                    enemy.TakeDamage(weaponConfiguration.GetDamageAmount());
                }
            }*/

            if (target != null)
            {
                Vector3 vectorToTarget = target.transform.position - m_characterCombatConfiguration.transform.position;
                float s = Vector2.Dot(new Vector2(vectorToTarget.x, vectorToTarget.y), new Vector2(m_characterCombatConfiguration.characterForward.x, m_characterCombatConfiguration.characterForward.y));
                if (s > 0)
                {
                    target.TakeDamage(weaponConfiguration.GetDamageAmount());
                }
            }
        }
    }
}
