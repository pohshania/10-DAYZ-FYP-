using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyStates
{
    // enemy
    private EnemyManager theEnemy;

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Enemy Chasing");
        //Chase();

        // if enemy spotted player and player still not dead
        if (theEnemy.Target != null && PlayerManager.Instance.IsDead == false)
        {
            // enemy move
            theEnemy.EnemyMove();

            Chase();

            // check if the player is at edge
            if (theEnemy.atWall == true || theEnemy.notAtEdge == false)
            {
                // ignore player and continue to patrol
                theEnemy.Target = null;
                theEnemy.ChangeState(new EnemyPatrolState());
            }
        }
        else // when player go out of enemy range or dead
        {
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

    private void Chase()
    {
        // set to patrol animation to enemy speed
        theEnemy.enemyAnim.SetFloat("Speed", theEnemy.MoveSpeed);

        float DirX = theEnemy.Target.transform.position.x - theEnemy.transform.position.x;

        if(DirX < 5f)
        {
            Debug.Log("PLAYER IN ATTACK RANGE");
            theEnemy.ChangeState(new EnemyAttackState());
        }
    }
}
