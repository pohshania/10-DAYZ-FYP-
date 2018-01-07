using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    [SerializeField]
    private EnemyManager theEnemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // make the player the enemy's target
            theEnemy.Target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // set target to null when exit the range box
            theEnemy.Target = null;
        }
    }
}
