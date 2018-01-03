using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Melee")
        {
            Debug.Log("You have hitted the skelel :< NUUUUUUUUUU T..T");

        }
    }
}
