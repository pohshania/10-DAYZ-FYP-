using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyStates
{
    void Execute();
    void Enter(EnemyManager enemy);
    void Exit();
    void OnTriggerEnter();
    void OnTriggerEnter2D(Collider2D other);

}
