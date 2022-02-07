using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Stat health;
    [SerializeField]
    protected Stat moveSpeed;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Sprite[] characterSprites;

    public void TakeDamage(float damageAmount)
    {
        health.amount -= damageAmount;
        if (health.amount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //drop object random
        Destroy(this);
    }
}
