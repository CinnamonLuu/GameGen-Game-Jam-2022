using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (GameManager.instance.player.transform.position - transform.position) * moveSpeed.amount * Time.deltaTime ;
    }
}
