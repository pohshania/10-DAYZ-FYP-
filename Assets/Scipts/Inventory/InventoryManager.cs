using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Singleton
    public static InventoryManager Instance { get; private set; }

    // UI
    [Header("Inventory UI")]
    private RectTransform _inventoryRect;
    private float _PanelWidth, _PanelHeight;
    [Tooltip("Number of inventory slots")]
    public int InventorySlots;
    [Tooltip("Number of inventory row")]
    public int InventoryRows;
    [Tooltip("Inventory slot paddings")]
    // The left space between each slot
    public float LeftPadding, TopPadding;
    [Tooltip("Each inventory slot size")]
    public float SlotSize;
    [Tooltip("Inventory slot prefab")]
    public GameObject SlotPrefab;
    private int InventoryColumn;

    private List<GameObject> AllInventorySlots;


    //[Header("Inventory Slots")]
    //[Tooltip("Total inventory slots")]
    //public int InventorySlots;

    [Header("Inventory List")]
    [Tooltip("Hold the items looted")]
    public List<ItemDatabase> Items;

    void Awake()
    {
        if (Instance == null) // if instance doesn't contain anything, this script is running for the first time
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
        Items = new List<ItemDatabase>();

        // display inventory UI layout
        //DisplayInventoryUI();
        DisplayInventoryUI2();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void DisplayInventoryUI()
    {
        // instantiate slots list
        AllInventorySlots = new List<GameObject>();

        _PanelWidth = (InventorySlots / InventoryRows) * (SlotSize + LeftPadding) + LeftPadding;

        _PanelHeight = InventoryRows * (SlotSize + TopPadding) + TopPadding;

        _inventoryRect = GetComponent<RectTransform>();

        _inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _PanelWidth);

        _inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _PanelHeight);

        InventoryColumn = InventorySlots / InventoryRows;

        // Runs through the rows
        for (int y = 0; y < InventoryRows; y++)
        {
            // Runs through the columns
            for (int x = 0; x < InventoryColumn; x++)
            {
                GameObject NewSlot = (GameObject)Instantiate(SlotPrefab);

                RectTransform SlotRect = NewSlot.GetComponent<RectTransform>();

                NewSlot.name = "Slot";

                NewSlot.transform.SetParent(this.transform.parent);

                SlotRect.localPosition = _inventoryRect.localPosition + new Vector3(LeftPadding * (x + 1) + (SlotSize * x), -TopPadding * (y + 1) - (SlotSize * y));


                SlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);

                SlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);

                AllInventorySlots.Add(NewSlot);
            }
        }
    }

    private void DisplayInventoryUI2()
    {

    }

    public bool AddItem(ItemDatabase item)
    {
        if(Items.Count >= InventorySlots)
        {
            Debug.Log("Not enough slots");
            return false;
        }

        Items.Add(item);
        return true;
    }

    public void RemoveItem(ItemDatabase item)
    {
        Items.Remove(item);
    }
}
