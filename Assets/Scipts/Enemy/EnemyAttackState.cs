using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStates
{
    // enemy
    private EnemyManager theEnemy;

    // timer
    private float _attackTimer = 1f;
    private float _attackCD;

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;

    }

    public void Execute()
    {
        Debug.Log("Enemy Attacking");

        // if player in enemy range and still alive
        if (theEnemy.Target != null && PlayerManager.Instance.IsDead == false) 
        {
            Attack();
        }
        else
        {
            // set to attack animation bool to false
            theEnemy.enemyAnim.SetBool("Attack", false);
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
        theEnemy.enemyAnim.SetBool("Attack", true);

        // start timer
        _attackCD -= Time.deltaTime;

        // after 2 sec resuming shooting
        if (_attackCD < 0)
        {
            _attackCD = _attackTimer;
            theEnemy.EnemyAttack();
        }
    }
}
