using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Health")]
    public float EnemyMaxHealth;
    public float EnemyCurrHealth;
    public GameObject EnemyHealthBar;

    [Header("Damage")]
    public int EnemyDamage;

    [Header("Movement")]
    public float MoveSpeed;
    public bool movingRight;

    [Header("Wall & Edge Check")]
    // check if enemy has partol till the edge or a wall
    public Transform WallCheck;
    public LayerMask WhatIsWall;
    public float WallCheckRadius;
    public bool atWall;

    public Transform EdgeCheck;
    public LayerMask WhatIsEdge;
    public float EdgeCheckRadius;
    public bool notAtEdge;

    [Header("Projectile")]
    public Transform EnemyFirePoint;
    public GameObject EnemyProjectileGO;

    [HideInInspector]
    public Animator enemyAnim;

    private float _scaleX;
    private float _scaleY;

    [HideInInspector]
    public bool IsDead;

    // Enemy state
    private EnemyStates _currState;

    public GameObject Target { get; set; }

    // Use this for initialization
    void Start ()
    {
        EnemyCurrHealth = EnemyMaxHealth;

        // enemy animator
        enemyAnim = GetComponent<Animator>();

        // enemy start with idle state
        ChangeState(new EnemyIdleState());

        // store enemy's x and y scale 
        _scaleX = transform.localScale.x;
        _scaleY = transform.localScale.y;

        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerManager.Instance.PlayerCurrHealth -= 10);

        _currState.Execute();

        LookAtPlayer();

        EnemyDeath();
	}

    public void LookAtPlayer()
    {
        if (Target != null)
        {
            Debug.Log("LOOK AT PLAYER!");
            // find the direction the player is at
            float DirX = Target.transform.position.x - transform.position.x;

            if (DirX < 0)
            {
                Debug.Log("player on left side");
                movingRight = false;
            }
            else if(DirX > 0)
            {
                Debug.Log("player on right side");
                movingRight = true;
            }
        }
    }

    public void ChangeState(EnemyStates newState)
    {
        if(_currState != null)
        {
            _currState.Exit();
        }

        _currState = newState;
        _currState.Enter(this);
    }

    public void EnemyMove()
    {
        atWall = Physics2D.OverlapCircle(WallCheck.position, WallCheckRadius, WhatIsWall);

        notAtEdge = Physics2D.OverlapCircle(EdgeCheck.position, WallCheckRadius, WhatIsWall);

        if (atWall || !notAtEdge) // switching
        {
            movingRight = !movingRight;
        }

        if (movingRight)
        {
            transform.localScale = new Vector3(-_scaleX, _scaleY, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(MoveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else // moving left
        {
            transform.localScale = new Vector3(_scaleX, _scaleY, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-MoveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void EnemyAttack()
    {

        //EnemyProjectileGO.GetComponent<Rigidbody2D>().velocity = new Vector2(EnemyProjectileSpeed, EnemyProjectileGO.GetComponent<Rigidbody2D>().velocity.y);
        //EnemyProjectileGO.GetComponent<Rigidbody2D>().angularVelocity = EnemyProjectileRotation;

        Instantiate(EnemyProjectileGO, EnemyFirePoint.position, EnemyFirePoint.rotation);
    }

    public void EnemyDeath()
    {
        // after 2 sec in Death state, destroy enemy
        if (IsDead == true)
        {
            Destroy(gameObject);

            Debug.Log("DESTROY THE ENEMY LAAAAA");

        }
    }

    void SetHealthBar(float health)
    {
        EnemyHealthBar.transform.localScale = new Vector3(Mathf.Clamp(health, 0f, 1f), EnemyHealthBar.transform.localScale.y, EnemyHealthBar.transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Melee")
        {
            Debug.Log("You have hitted the enemy :< NUUUUUUUUUU T..T");
            EnemyCurrHealth -= PlayerManager.Instance.PlayerMeleeDamage;

            float health = EnemyCurrHealth / EnemyMaxHealth;
            SetHealthBar(health);

            if (EnemyCurrHealth <= 0)
            {
                // ENEMY DIE LIAO THEN KENNA DESTROYED HERE
                ChangeState(new EnemyDeathState());
            }
        }
        
        if(other.tag == "Player")
        {
            Debug.Log("Enemy touched player!");
            // knock back player
            PlayerManager.Instance.KnockBackCount = PlayerManager.Instance.KnockBackLength;
            if (other.transform.position.x < transform.position.x)
            {
                PlayerManager.Instance.KnockFromRight = true;
            }
            else
            {
                PlayerManager.Instance.KnockFromRight = false;
            }
        }
    }
}
