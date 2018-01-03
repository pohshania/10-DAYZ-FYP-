using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    // Enemy's health
    public float EnemyMaxHealth;
    public float EnemyCurrHealth;
    public GameObject EnemyHealthBar;

    // Enemy's damage
    public int EnemyDamage;

    // Use this for initialization
    void Start ()
    {
        EnemyCurrHealth = EnemyMaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
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
