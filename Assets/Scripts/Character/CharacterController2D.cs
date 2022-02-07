using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Sprite[] characterSprites;

    [SerializeField]
    private float m_characterVelocity = 0.03f;
    //[SerializeField]
    public float m_dashCooldown = 5f;
    private float dashDuration = 0.2f;

    private float m_timeSienceLastDash = 0;
    [SerializeField]
    private float m_DashMultiplier;
    private float m_dashVelocity => m_characterVelocity * m_DashMultiplier;

    private bool inDash;
    private Vector3 goalPosition;

    public Rigidbody2D Body;

    
    public Animator animator;



    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("Backward",true);
        animator.SetBool("Forward",false);
        animator.SetBool("Left",false);
        animator.SetBool("Right",false);
        m_spriteRenderer.sprite = characterSprites[0];
    }

    // Update is called once per frame
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
            animator.SetBool("Backward",true);
            animator.SetBool("Forward",false);
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            m_spriteRenderer.sprite = characterSprites[0];
            if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
            {
                if (mousePos.x > 0.1)
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",true);
                    m_spriteRenderer.sprite = characterSprites[3];
                }
                else if (mousePos.x <= -0.1)
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",true);
                    animator.SetBool("Right",false);
                    m_spriteRenderer.sprite = characterSprites[2];
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
            if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
            {

                if (mousePos.x > 0.1)
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",true);
                    m_spriteRenderer.sprite = characterSprites[3];
                }
                else if (mousePos.x <= -0.1)
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",true);
                    animator.SetBool("Right",false);
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
                Body.velocity = vectorToAdd * m_dashVelocity;
                //goalPosition = transform.position + (Vector3.zero * m_dashVelocity);
                m_timeSienceLastDash = 0;
                Debug.Log("dash");
                animator.SetBool("Dash",true);
            }
            else
            {

                vectorToAdd *= m_characterVelocity;
                //transform.position += vectorToAdd;
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

    }
}
