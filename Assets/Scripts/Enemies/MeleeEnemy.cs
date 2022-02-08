using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public Animator animator;

    // Update is called once per frame
    protected override void Update()
    {
         animator.SetBool("Attack",false);
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

            animator.SetBool("Attack",true);
            return;
        }
        _body.velocity = direction * moveSpeed.amount ;

        if(_body.velocity[0] != 0 || _body.velocity[1] != 0){

            animator.SetBool("Quieto",false);

            if(_body.velocity[0] > 0)
            {
                if(_body.velocity[0]> Mathf.Abs(_body.velocity[1]))
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",true);
                }else if(Mathf.Sign(_body.velocity[1])==1)
                {
                    animator.SetBool("Backward",true);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",false);
                }else{
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",true);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",false);
                } 
            }else if(_body.velocity[0] < 0)
                {
                if(Mathf.Abs(_body.velocity[0])> Mathf.Abs(_body.velocity[1]))
                {
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",true);
                    animator.SetBool("Right",false);
                }else if(Mathf.Sign(_body.velocity[1])==1)
                {
                    animator.SetBool("Backward",true);
                    animator.SetBool("Forward",false);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",false);
                }else{
                    animator.SetBool("Backward",false);
                    animator.SetBool("Forward",true);
                    animator.SetBool("Left",false);
                    animator.SetBool("Right",false);
                } 

            }
        }else{
            animator.SetBool("Quieto",true);
        }
        
    }
}
