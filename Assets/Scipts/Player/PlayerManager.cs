﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance { get; private set; }

    // Player's info
    [Header("Name")]
    public string PlayerName;

    [Header("Components")]
    public int PlayerMaxHealth;
    public int PlayerCurrHealth;
    public int PlayerMaxHunger;
    public int PlayerCurrHunger;
    public int PlayerMaxThirst;
    public int PlayerCurrThirst;

    [Header("Movement")]
    public float PlayerMoveSpeed;
    public float PlayerJumpHeight;
    private float playerMoveVelocity;
    private float _scaleX;
    private float _scaleY;
    private float _scaleZ;
    private float _posX;
    private float _posY;
    private float _posZ;


    [Header("Ground Check")]
    // player jump to check on ground
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    public float GroundCheckRadius;
    public bool isGrounded;

    [Header("Attack")]
    public int PlayerMeleeDamage;
    public GameObject MeleeAttackPos;

    [Header("Cool Downs")]
    public float ChangeSceneCD;

    // sprite animation
    private Animator _playerAnim;

    // This is called before Start()
    private void Awake()
    {
        if(Instance == null) // if instance doesn't contain anything, this script is running for the first time
        {
            Instance = this; // set this curr instance to be contained inside ^ the static instance
            DontDestroyOnLoad(gameObject); // don't detroy the first instance
        }
        else
        {
            Destroy(gameObject); // destory other duplicated instances
        }

    }

	// Use this for initialization
	void Start ()
    {
        // store player's x and y scale 
        _scaleX = transform.localScale.x;
        _scaleY = transform.localScale.y;

        // store player's x and y and z pos 
        _posX = transform.position.x;
        _posY = transform.position.y;
        _posZ = transform.position.z;

        // player animation
        _playerAnim = GetComponent<Animator>();

        // player components
        PlayerCurrHealth = PlayerMaxHealth;
        PlayerCurrHunger = PlayerMaxHunger;
        PlayerCurrThirst = PlayerMaxThirst;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);
    }

    // Update is called once per frame
    void Update ()
    {
        // player direction flip
        if (playerMoveVelocity > 0)
        {
            // player flip to right 
            transform.localScale = new Vector3(_scaleX, _scaleY, 1f);
        }
        else if (playerMoveVelocity < 0)
        {
            // player flip to left
            transform.localScale = new Vector3(-_scaleX, _scaleY, 1f);
        }

        // player walking
        PlayerWalk();
        // player jumping
        PlayerJump();
        // player melee attacking
        PlayerMelee();

        // player die
        //PlayerDead();

        // Debugging
        AddHealth();
    }

    // Player walk
    void PlayerWalk()
    {
        playerMoveVelocity = PlayerMoveSpeed * Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(playerMoveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        // walking animation
        _playerAnim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    // Player jump
    void PlayerJump()
    {
        // player jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, PlayerJumpHeight);
            //Debug.Log("jump!");
        }

        // jumping animation
        // the jump anim boolean is set to the same value as the isGrounded value in here
        _playerAnim.SetBool("Grounded", isGrounded);

        // print out if player is on ground
        // Debug.Log(isGrounded);
    }

    // Player melee
    void PlayerMelee()
    {
        // player attacking
        if (_playerAnim.GetBool("Melee") == true)
        {
            _playerAnim.SetBool("Melee", false);
            // Debug.Log("ATTTTTTTTACK finish");
        }
        else if(_playerAnim.GetBool("Melee") == false && isGrounded == true)
        {
            MeleeAttackPos.SetActive(false);  // toggle off melee attack
            _playerAnim.SetBool("JumpMelee", false);
        }

        // melee attack when standing still
        if (Input.GetButtonDown("Fire1") && playerMoveVelocity == 0 && isGrounded == true)  //if(Input.GetKeyDown(KeyCode.X))
        {
            _playerAnim.SetBool("Melee", true);
            MeleeAttackPos.SetActive(true);  // toggle on melee attack pos
        }
        else if(Input.GetButtonDown("Fire1") && isGrounded == false) // jump melee attack animation
        {
            _playerAnim.SetBool("JumpMelee", true);
            MeleeAttackPos.SetActive(true);  // toggle on melee attack pos
        }
    }

    // Player dead
    void PlayerDead()
    {
        if(PlayerCurrHealth <= 0)
        {
            Debug.Log("this ding dong xi kiao kiao alr");

            _playerAnim.SetBool("Dead", true);

            transform.position = new Vector3(transform.position.x, _posY - 0.2f, transform.position.z);

            ChangeSceneCD -= Time.deltaTime;

            if (ChangeSceneCD < 0)
            {
                // back to character select
                SceneManager.LoadScene(0);
                

                Destroy(gameObject);
            }
            
        }
    }

    // Add health
    void AddHealth()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            PlayerCurrHealth += 20;
        }
    }

    // Getters
    public int GetPlayerCurrHealth()
    {
        return PlayerCurrHealth;
    }
    public int GetPlayerCurrHunger()
    {
        return PlayerCurrHunger;
    }
    public int GetPlayerCurrThirst()
    {
        return PlayerCurrThirst;
    }
}
