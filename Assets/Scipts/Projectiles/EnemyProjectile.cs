using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Damage")]
    public int EnemyProjectileDamage;

    [Header("Speed")]
    public float EnemyProjetileSpeed;

    [Header("Rotation")]
    public float EnemyProjetileRotationSpeed;

    private PlayerManager player;
    private Rigidbody2D myRigidBody2D;

    // Use this for initialization
    void Start ()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayerManager>();

        // if facing right velocity - X
        if (player.transform.position.x < transform.position.x)
        {
            EnemyProjetileSpeed = -EnemyProjetileSpeed;
            EnemyProjetileRotationSpeed = -EnemyProjetileRotationSpeed;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        myRigidBody2D.velocity = new Vector2(EnemyProjetileSpeed, myRigidBody2D.velocity.y);

        myRigidBody2D.angularVelocity = EnemyProjetileRotationSpeed;
    }

    // when the renderer is no longer visible by any camera
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.Instance.PlayerCurrHealth -= EnemyProjectileDamage;   
        }

        if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
