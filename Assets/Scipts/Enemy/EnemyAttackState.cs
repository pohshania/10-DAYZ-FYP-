using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStates
{
    private EnemyManager theEnemy;

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;

    }

    public void Execute()
    {
        Debug.Log("Enemy Attacking");

        // if player in enemy range
        if (theEnemy.Target != null) 
        {
            Attack();
        }
        else
        {
            // set to attack animation bool to false
            theEnemy.enemyAnim.SetBool("attack", false);
            theEnemy.ChangeState(new EnemyIdleState());
        }


    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void Attack()
    {
        // set to attack animation bool to true
        theEnemy.enemyAnim.SetBool("attack", true);
    }
}
