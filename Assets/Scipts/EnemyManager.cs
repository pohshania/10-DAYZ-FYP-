using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    // enemy states
    enum ENEMY_STATE
    {
        PATROL = 0,
        CHASE,
        ATTACK,
        DEATH,
        NONE
    }

    ENEMY_STATE states;

    private PlayerManager _player;
    public float PlayerRange;

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

    private float _scaleX;
    private float _scaleY;

    [Header("Player in range")]
    public LayerMask PlayerLayer;
    [SerializeField]
    private bool playerInRange;

    // Use this for initialization
    void Start ()
    {
        EnemyCurrHealth = EnemyMaxHealth;

        // find player in scene
        _player = FindObjectOfType<PlayerManager>();

        // store player's x and y scale 
        _scaleX = transform.localScale.x;
        _scaleY = transform.localScale.y;

        // enemy state set as patrol first
        states = ENEMY_STATE.PATROL;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // enemy state
        switch(states)
        {
            case ENEMY_STATE.PATROL:
                {
                    EnemyPatrol();
                    break;
                }

            case ENEMY_STATE.CHASE:
                {
                    EnemyChase();
                    break;
                }

            case ENEMY_STATE.ATTACK:
                {
                    EnemyAttack();
                    break;
                }

            case ENEMY_STATE.DEATH:
                {
                    EnemyDeath();
                    break;
                }
        }

        //EnemyChase();
        //EnemyPatrol();
        
	}

    void SetHealthBar(float health)
    {
        EnemyHealthBar.transform.localScale = new Vector3(Mathf.Clamp(health, 0f, 1f), EnemyHealthBar.transform.localScale.y, EnemyHealthBar.transform.localScale.z);
    }

    void EnemyPatrol()
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

        // check if player is in range
        playerInRange = Physics2D.OverlapCircle(transform.position, PlayerRange, PlayerLayer);
        if(playerInRange)
        {
            states = ENEMY_STATE.CHASE;
        }
    }

    void EnemyChase()
    {
        if(notAtEdge && !atWall)
        {
            Debug.Log("CHASING PLAYER NOW!!");
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, MoveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("CHASE TIL EDGE!!");
            states = ENEMY_STATE.ATTACK;
        }
    }

    void EnemyAttack()
    {

    }

    void EnemyDeath()
    {

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
                Destroy(gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, PlayerRange);
    }
}
