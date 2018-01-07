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

    [HideInInspector]
    public Animator enemyAnim;

    private float _scaleX;
    private float _scaleY;

    // Enemy state
    private EnemyStates _currState;

    // Use this for initialization
    void Start ()
    {
        EnemyCurrHealth = EnemyMaxHealth;

        // enemy animator
        enemyAnim = GetComponent<Animator>();

        // enemy start with idle state
        ChangeState(new EnemyIdleState());

        // store player's x and y scale 
        _scaleX = transform.localScale.x;
        _scaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        _currState.Execute();
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
                Destroy(gameObject);
            }
        }
    }
}
