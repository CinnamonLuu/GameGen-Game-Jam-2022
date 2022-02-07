using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector3 direction = (player.gameObject.transform.position - transform.position).normalized;
        if(!(Vector3.Distance(GameManager.instance.player.transform.position, transform.position) > range.amount* GameManager.instance.cellSize))
        {
            //Attack
            return;
        }
        _body.velocity = direction * moveSpeed.amount ;
    }
}
