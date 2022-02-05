using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle - 90, 0));

        Vector3 vectorToAdd = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            vectorToAdd.x -= 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vectorToAdd.x += 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vectorToAdd.z += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vectorToAdd.z -= 1f;
        }

        transform.position += vectorToAdd.normalized * 0.01f;
    }
}
