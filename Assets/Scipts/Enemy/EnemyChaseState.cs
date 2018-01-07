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
        Chase();

        // if enemy spotted player 
        if (theEnemy.Target != null)
        {
            theEnemy.ChangeState(new EnemyChaseState());
        }
        else // when player go out of enemy range
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
        // enemy move
        theEnemy.EnemyMove();

        // set to patrol animation to enemy speed
        theEnemy.enemyAnim.SetFloat("speed", theEnemy.MoveSpeed);
    }
}
