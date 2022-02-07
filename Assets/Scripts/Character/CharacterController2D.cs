using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private Stat health;

    [Header("Weapon")]
    [SerializeField]
    private Base_WeaponBehaviour m_weapon;

    [Header("Sprites")]
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Sprite[] characterSprites;
    [Header("Movement variables")]
    [SerializeField]
    private float m_characterVelocity = 0.03f;
    [SerializeField]
    private float m_dashCooldown = 5f;
    [SerializeField]
    private float m_DashMultiplier;


    private float dashDuration = 0.2f;
    private float m_timeSienceLastDash = 0f;
    private bool inDash;
    private Vector3 goalPosition;

    private float m_dashVelocity => m_characterVelocity * m_DashMultiplier;
    public float Health => health.amount;
    public Rigidbody2D Body;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = characterSprites[0];
    }

    void Update()
    {
        m_timeSienceLastDash += Time.deltaTime;

        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.Normalize();
        if (mousePos.y > 0.1)
        {
            m_spriteRenderer.sprite = characterSprites[0];
            if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
            {
                if (mousePos.x > 0.1)
                {

                    m_spriteRenderer.sprite = characterSprites[3];
                }
                else if (mousePos.x <= -0.1)
                {

                    m_spriteRenderer.sprite = characterSprites[2];
                }
            }
        }
        else if (mousePos.y <= -0.1)
        {
            m_spriteRenderer.sprite = characterSprites[1];
            if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
            {

                if (mousePos.x > 0.1)
                {

                    m_spriteRenderer.sprite = characterSprites[3];
                }
                else if (mousePos.x <= -0.1)
                {

                    m_spriteRenderer.sprite = characterSprites[2];
                }
            }
        }
        if (!inDash)
        {
            Debug.Log("me muevo");
            Vector3 vectorToAdd = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                vectorToAdd.y += 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                vectorToAdd.y -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                vectorToAdd.x += 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                vectorToAdd.x -= 1f;
            }

            vectorToAdd.Normalize();

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && m_timeSienceLastDash >= m_dashCooldown)
            {
                inDash = true;
                goalPosition = transform.position + (vectorToAdd * m_dashVelocity);
                m_timeSienceLastDash = 0;
                Debug.Log("dash");
            }
            else
            {

                vectorToAdd *= m_characterVelocity;
                //transform.position += vectorToAdd;
                Body.velocity = vectorToAdd;
            }
        }
        else
        {
            Debug.Log(m_timeSienceLastDash);
            transform.position = Vector3.Lerp(transform.position, goalPosition, m_timeSienceLastDash / dashDuration);
            if (m_timeSienceLastDash >= dashDuration)
            {
                inDash = false;
            }
        }

    }

    public void Attack()
    {
        if (m_weapon != null)
        {
            m_weapon.Attack();
        }
    }

    public void GetDamage(float amount)
    {
        health.amount -= amount;
        if (health.amount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //finish game
    }
}
