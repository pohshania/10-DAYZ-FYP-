using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<ItemDatabase> Items;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("More than 1 instance of inventory");
            return;
        }

        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        Items = new List<ItemDatabase>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddItem(ItemDatabase item)
    {
        Items.Add(item);
    }

    public void RemoveItem(ItemDatabase item)
    {
        Items.Remove(item);
    }
}
