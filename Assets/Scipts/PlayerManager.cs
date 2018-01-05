using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [Header("Ground Check")]
    // player jump to check on ground
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    public float GroundCheckRadius;
    public bool isGrounded;

    [Header("Attack")]
    public int PlayerMeleeDamage;
    public GameObject MeleeAttackPos;

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

}
