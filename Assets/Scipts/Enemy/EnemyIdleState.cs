using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStates
{
    private EnemyManager theEnemy;

    private float _idleTimer; // contain the time enemy idle
    private float _idleDuration = 5f; // time stay in idle    

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Enemy Idling");
        Idle();
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
        // set to idle animation
        theEnemy.enemyAnim.SetFloat("speed", 0);

        _idleTimer += Time.deltaTime;

        if(_idleTimer >= _idleDuration)
        {
            theEnemy.ChangeState(new EnemyPatrolState());
        }
    }
}
