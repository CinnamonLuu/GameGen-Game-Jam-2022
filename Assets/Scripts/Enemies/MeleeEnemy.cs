using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{


    // Update is called once per frame
    private void Update()
    {
        combatConfiguration.animator.SetBool("Attack",false);
        //combatConfiguration.animator.SetBool("Damage",false);

        combatConfiguration.Elapsed += Time.deltaTime;
        if (localization.room != player.localization.room)
        {
            _body.velocity = Vector2.zero;
            combatConfiguration.animator.SetBool("Quieto", true);
            return;
        }

        if (!player)
        {
            combatConfiguration.animator.SetBool("Quieto", true);
            return;
        }

        Vector3 direction = (player.gameObject.transform.position - transform.position).normalized;

        if (Mathf.Abs(direction.x ) > Mathf.Abs(direction.y))
        {
            combatConfiguration.characterForward = direction.x > 0 ? Vector2.right : Vector2.left;
        }
        else
            combatConfiguration.characterForward = direction.y > 0 ? Vector2.up : Vector2.down;

        

        if(!(Vector3.Distance(GameManager.instance.player.transform.position, transform.position) > combatConfiguration.range.amount* GameManager.instance.cellSize))
        {
            //Attack
/*            Debug.Log("Player in range:" + combatConfiguration.Elapsed);
            Debug.Log(combatConfiguration.Elapsed >= 1 / combatConfiguration.attackSpeed.amount);*/
            if (combatConfiguration.Elapsed >= 1/combatConfiguration.attackSpeed.amount)
            {
                Debug.Log("Attack player");
                combatConfiguration.Weapon.Attack();
                combatConfiguration.Elapsed = 0;
                combatConfiguration.animator.SetBool("Attack", true);
            }
            return;
        }

        _body.velocity = direction * moveSpeed.amount ;

        if(_body.velocity[0] != 0 || _body.velocity[1] != 0){

            combatConfiguration.animator.SetBool("Quieto",false);

            if(_body.velocity[0] > 0)
            {
                if(_body.velocity[0]> Mathf.Abs(_body.velocity[1]))
                {
                    combatConfiguration.animator.SetBool("Backward",false);
                    combatConfiguration.animator.SetBool("Forward",false);
                    combatConfiguration.animator.SetBool("Left",false);
                    combatConfiguration.animator.SetBool("Right",true);
                }else if(Mathf.Sign(_body.velocity[1])==1)
                {
                    combatConfiguration.animator.SetBool("Backward",true);
                    combatConfiguration.animator.SetBool("Forward",false);
                    combatConfiguration.animator.SetBool("Left",false);
                    combatConfiguration.animator.SetBool("Right",false);
                }else{
                    combatConfiguration.animator.SetBool("Backward",false);
                    combatConfiguration.animator.SetBool("Forward",true);
                    combatConfiguration.animator.SetBool("Left",false);
                    combatConfiguration.animator.SetBool("Right",false);
                } 
            }else if(_body.velocity[0] < 0)
                {
                if(Mathf.Abs(_body.velocity[0])> Mathf.Abs(_body.velocity[1]))
                {
                    combatConfiguration.animator.SetBool("Backward",false);
                    combatConfiguration.animator.SetBool("Forward",false);
                    combatConfiguration.animator.SetBool("Left",true);
                    combatConfiguration.animator.SetBool("Right",false);
                }else if(Mathf.Sign(_body.velocity[1])==1)
                {
                    combatConfiguration.animator.SetBool("Backward",true);
                    combatConfiguration.animator.SetBool("Forward",false);
                    combatConfiguration.animator.SetBool("Left",false);
                    combatConfiguration.animator.SetBool("Right",false);
                }else{
                    combatConfiguration.animator.SetBool("Backward",false);
                    combatConfiguration.animator.SetBool("Forward",true);
                    combatConfiguration.animator.SetBool("Left",false);
                    combatConfiguration.animator.SetBool("Right",false);
                } 

            }
        }else{
            combatConfiguration.animator.SetBool("Quieto",true);
        }
        
    }
}
