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
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = characterSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
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

                    m_spriteRenderer.sprite = characterSprites[2];
                }
                else if (mousePos.x <= -0.1)
                {

                    m_spriteRenderer.sprite = characterSprites[3];
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

                    m_spriteRenderer.sprite = characterSprites[2];
                }
                else if (mousePos.x <= -0.1)
                {

                    m_spriteRenderer.sprite = characterSprites[3];
                }
            }
        }

        Vector3 vectorToAdd = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            vectorToAdd.z -= 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vectorToAdd.z += 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vectorToAdd.x -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vectorToAdd.x += 1f;
        }

        transform.position += vectorToAdd.normalized * 0.03f;
    }
}
