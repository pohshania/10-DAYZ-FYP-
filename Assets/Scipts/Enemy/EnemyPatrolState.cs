using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStates
{
    private float _patrolTimer;
    private float _patrolDuration = 10f;

    private EnemyManager theEnemy;

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Enemy is patroling");
        Patrol();

        theEnemy.EnemyMove();
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
        // set to patrol animation to enemy speed
        theEnemy.enemyAnim.SetFloat("speed", theEnemy.MoveSpeed);

        _patrolTimer += Time.deltaTime;

        if (_patrolTimer >= _patrolDuration)
        {
            theEnemy.ChangeState(new EnemyIdleState());
        }
    }
}
