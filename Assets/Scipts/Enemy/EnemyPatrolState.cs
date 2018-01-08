using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStates
{
    // enemy
    private EnemyManager theEnemy;

    // timer
    private float _patrolTimer;
    private float _patrolDuration = 10f;

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Enemy Patroling");
        Patrol();

        if (theEnemy.Target != null)
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

    private void Patrol()
    {
        // enemy move
        theEnemy.EnemyMove();

        // set to patrol animation to enemy speed
        theEnemy.enemyAnim.SetFloat("Speed", theEnemy.MoveSpeed);

        _patrolTimer += Time.deltaTime;

        // after 10 sec of Patrolling change state to Idle
        if (_patrolTimer >= _patrolDuration)
        {
            _patrolTimer = 0f;
            theEnemy.ChangeState(new EnemyIdleState());
        }
    }
}
