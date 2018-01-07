using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStates
{
    private EnemyManager theEnemy;

    private float _attackTimer = 3f;
    private float _attackCD;
    private bool canAttack;

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Enemy Attacking");
        //EnemyAttack();

       // if the enemy has a target, spotted player
       if(theEnemy.Target != null)
        {
            theEnemy.EnemyMove();
        }
        else
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

    private void EnemyAttack()
    {
        _attackTimer += Time.deltaTime;

        if(_attackTimer >= _attackCD)
        {
            canAttack = true;
            _attackTimer = 0f;
        }

        if(canAttack)
        {
            canAttack = !canAttack;
            theEnemy.enemyAnim.SetTrigger("throw");
        }
    }
}
