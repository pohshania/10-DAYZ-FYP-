using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStates
{
    // enemy
    private EnemyManager theEnemy;
    
    // timer
    private float _idleTimer; 
    private float _idleDuration = 5f;   

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Enemy Idling");
        Idle();

        // if enemy spotted player 
        if(theEnemy.Target != null)
        {
            theEnemy.ChangeState(new EnemyChaseState());
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

    private void Idle()
    {
        // set enemy to idle animation
        theEnemy.enemyAnim.SetFloat("Speed", 0);

        // start timer
        _idleTimer += Time.deltaTime;
        // after 5 sec of Idling change state to Patrol
        if(_idleTimer >= _idleDuration)
        {
            _idleTimer = 0f;
            theEnemy.ChangeState(new EnemyPatrolState());
        }
    }
}
