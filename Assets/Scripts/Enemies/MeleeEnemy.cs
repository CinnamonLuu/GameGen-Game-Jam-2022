using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    // Update is called once per frame
    protected override void Update()
    {
        if (localization.room != player.localization.room)
        {
            _body.velocity = Vector2.zero;
            return;
        }

        Vector3 direction = (player.gameObject.transform.position - transform.position).normalized;

        if (Mathf.Abs(direction.x ) > Mathf.Abs(direction.y))
        {
            characterForward = direction.x > 0 ? Vector2.right : Vector2.left;
        }
        else
            characterForward = direction.y > 0 ? Vector2.up : Vector2.down;

        if(!(Vector3.Distance(GameManager.instance.player.transform.position, transform.position) > range.amount* GameManager.instance.cellSize))
        {
            //Attack
            m_weapon.Attack();
            return;
        }
        _body.velocity = direction * moveSpeed.amount ;
    }
}
