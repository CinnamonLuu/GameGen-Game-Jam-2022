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
    private Base_WeaponConfiguration m_weaponConfiguration;
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
    //[SerializeField]
    public float m_dashCooldown = 5f;
    private float dashDuration = 0.2f;

    private float m_timeSienceLastDash = 0;
    [SerializeField]
    private float m_DashMultiplier;

    private bool inDash;
    private Vector3 goalPosition;

    private float m_dashVelocity => m_characterVelocity * m_DashMultiplier;
    public float Health => health.amount;
    public Rigidbody2D Body;
    public Localization localization;

    public Vector3 characterForward;

    
    public Animator animator;
    public ParticleSystem dust;



    void Start()
    {
        
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("Backward",true);
        animator.SetBool("Forward",false);
        animator.SetBool("Left",false);
        animator.SetBool("Right",false);
        m_spriteRenderer.sprite = characterSprites[0];
        m_weapon = m_weaponConfiguration.InstantiateWeaponBehaviour();
        m_weapon.SetCharacter(this);
        characterForward = transform.up;
    }

    void Update()
    {
        animator.SetBool("Attack",false);
        animator.SetBool("Sword",false);
        animator.SetBool("Shield",false);
        animator.SetBool("Bow",false);

         animator.SetBool("Damage",false);

        m_timeSienceLastDash += Time.deltaTime;

        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.Normalize();
        if (mousePos.y > 0.1)
        {
            animator.SetBool("Backward",true);
            animator.SetBool("Forward",false);
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            m_spriteRenderer.sprite = characterSprites[0];
            characterForward = transform.up;
            if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
            {
                if (mousePos.x > 0.1)
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",true);
                    m_spriteRenderer.sprite = characterSprites[3];
                    characterForward = transform.right;
                }
                else if (mousePos.x <= -0.1)
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",true);
                    animator.SetBool("Right",false);
                    m_spriteRenderer.sprite = characterSprites[2];
                    characterForward = -transform.right;
                }
            }
        }
        else if (mousePos.y <= -0.1)
        {
            animator.SetBool("Backward",false);
            animator.SetBool("Forward",true);
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            m_spriteRenderer.sprite = characterSprites[1];
            characterForward = -transform.up;
            if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
            {
                if (mousePos.x > 0.1)
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",true);
                    m_spriteRenderer.sprite = characterSprites[3];
                    characterForward = transform.right;
                }
                else if (mousePos.x <= -0.1)
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",true);
                    animator.SetBool("Right",false);
                    m_spriteRenderer.sprite = characterSprites[2];
                    characterForward = -transform.right;
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
                Body.velocity = vectorToAdd * m_dashVelocity;
                //goalPosition = transform.position + (Vector3.zero * m_dashVelocity);
                m_timeSienceLastDash = 0;
                Debug.Log("dash");
                animator.SetBool("Dash",true);
                CreateDust();
            }
            else
            {

                vectorToAdd *= m_characterVelocity;
                Body.velocity = vectorToAdd;

                if(Body.velocity[0] == 0f && Body.velocity[1] == 0f)
                {
                    animator.SetBool("Quieto",true);
                }else{
                    animator.SetBool("Quieto",false);
                }
                
            }
        }
        else
        {
            Debug.Log(m_timeSienceLastDash);
            //transform.position = Vector3.Lerp(transform.position, goalPosition, m_timeSienceLastDash / dashDuration);
            if (m_timeSienceLastDash >= dashDuration)
            {
                inDash = false;
                animator.SetBool("Dash",false);
                
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

    }

    public void Attack()
    {
        Debug.Log("ataco");
        if (m_weapon != null)
        {
            animator.SetBool("Attack",true);
            m_weapon.Attack();
            
            animator.SetBool("Sword",true);
        }
    }

    public void GetDamage(float amount)
    {
        health.amount -= amount;
        animator.SetBool("Damage",true);
        if (health.amount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //finish game
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 8);
    }

    void CreateDust(){
        dust.Play();
    }
}
