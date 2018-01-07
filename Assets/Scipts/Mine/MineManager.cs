using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour {

    public float MineMaxHealth;
    public float MineCurrHealth;
    public GameObject MineHealthBar;

	// Use this for initialization
	void Start ()
    {
        MineCurrHealth = MineMaxHealth;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SetHealthBar(float health)
    {
        MineHealthBar.transform.localScale = new Vector3(Mathf.Clamp(health, 0f, 1f), MineHealthBar.transform.localScale.y, MineHealthBar.transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Melee")
        {
            Debug.Log("You have hitted the mine :< NUUUUUUUUUU T..T");
            MineCurrHealth -= PlayerManager.Instance.PlayerMeleeDamage;

            float health = MineCurrHealth / MineMaxHealth;
            SetHealthBar(health);

            if(MineCurrHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
