using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStates
{
    private EnemyManager theEnemy;
    private float _scaleX;
    private float _scaleY;

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;

        _scaleX = theEnemy.transform.localScale.x;
        _scaleY = theEnemy.transform.localScale.y;
    }

    public void Execute()
    {
        Debug.Log("Enemy Attacking");

        // if player go out of range
        if (theEnemy.Target == null) 
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

}
