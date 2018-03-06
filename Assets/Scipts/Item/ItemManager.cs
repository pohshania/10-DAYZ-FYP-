using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class handles the gameobject items
public class ItemManager : MonoBehaviour
{
    [Header("Item info")]
    public ItemDatabase Item;
    public Text ItemDescription;
    public float ItemScale;

    // Use this for initialization
    void Start ()
    {
        // set item's sprite
        GetComponent<SpriteRenderer>().sprite = Item.ItemSprite;
        // set item's scale
        transform.localScale = new Vector3(ItemScale, ItemScale, 1f);
        // set item's description
        //ItemDescription.GetComponent<Text>().text = Item.ItemDescription;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            InventoryManager.Instance.AddItemToSlot(Item);
        }
    }
}
