using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public ItemDatabase Item;
    public Text ItemDescription;
    public float ItemScale;
    private Vector3 _scale;
    private bool _itemLooted;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _itemLooted = InventoryManager.Instance.AddItem(Item);

            if(_itemLooted)
            {
                Destroy(gameObject);
            }
        }
    }
}
