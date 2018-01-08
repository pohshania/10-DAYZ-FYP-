using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyStates
{
    // enemy
    private EnemyManager theEnemy;

    // timer
    private float _deathTimer;
    private float _deathDuration = 2f;

    public void Enter(EnemyManager enemy)
    {
        this.theEnemy = enemy;
    }

    public void Execute()
    {
        //Debug.Log("Enemy Dead!");
        Death();

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

    private void Death()
    {
        // set enemy animation to death 
        theEnemy.enemyAnim.SetBool("Death", true);

        //Debug.Log("Enemy Ded Anim");

        _deathTimer += Time.deltaTime;

        // after 2 sec of Death state, destroy enemy
        if (_deathTimer >= _deathDuration)
        {
            // set bool IsDead true to toggle destroy enemy GO
            theEnemy.IsDead = true;
            _deathTimer = _deathDuration;
        }
    }
}
