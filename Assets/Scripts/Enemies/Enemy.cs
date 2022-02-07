using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Localization))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Stat health;
    [SerializeField]
    protected Stat moveSpeed;
    [SerializeField]
    protected Stat range;

    [SerializeField]
    protected Rigidbody2D _body;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Sprite[] characterSprites;

    [SerializeField]
    protected Localization localization;

    protected CharacterController2D player;

    public Vector2 characterForward;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
    }


    protected virtual void Update()
    {
        


    }
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
        Destroy(gameObject);
    }
}
