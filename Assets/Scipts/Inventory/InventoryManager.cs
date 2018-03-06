using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Singleton
    public static InventoryManager Instance { get; private set; }

    // UI
    [Header("Inventory UI")]
    [Tooltip("Number of inventory slots")]
    public int InventorySlots;
    [Tooltip("The left space between each slot")]
    public float LeftPadding;
    [Tooltip("Each inventory slot size")]
    public float SlotSize;
    [Tooltip("Inventory slot prefab")]
    public GameObject SlotPrefab;

    // Holds all the slots
    private List<GameObject> AllInventorySlots;

    // Shows the number of empty slots in the inventory
    private static int _emptySlots;
    public static int EmptySlots
    {
        get { return _emptySlots; }
        set { _emptySlots = value; }
    }

    private static SlotManager _slotMoveFrom, _slotMoveTo;

    public EventSystem eventSystem;

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
        // display inventory UI layout
        DisplayInventoryUI();
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(!eventSystem.IsPointerOverGameObject(-1) && _slotMoveFrom != null)
            {
                _slotMoveFrom.GetComponent<Image>().color = Color.white;
                _slotMoveFrom.ClearSlot();
                _slotMoveTo = null;
                _slotMoveFrom = null;
            }
        }
    }

    private void DisplayInventoryUI()
    {
        // Instantiate slots list
        AllInventorySlots = new List<GameObject>();

        // The amount of empty slot at start is the same as the number of slots
        _emptySlots = InventorySlots;

        // UI calculation
        for (int i = 0; i < InventorySlots; i++)
        {
            GameObject NewSlot = (GameObject)Instantiate(SlotPrefab);
      
            NewSlot.transform.SetParent(this.transform);

            NewSlot.transform.localScale = new Vector3(SlotSize,SlotSize);

            float posX = (this.transform.position.x) + (((i - 1) + 1) * LeftPadding);

            // slots' position
            NewSlot.transform.localPosition = new Vector3(posX, this.transform.position.y);

            AllInventorySlots.Add(NewSlot);
        }

        Debug.Log("Inventory displayed!");
    }

    // Run through all the slots to check if it is empty
    // If so, add item into new empty slot
    private bool PlaceIntoEmptySlot(ItemDatabase item)
    {
        // if there is still empty slot left
        if(_emptySlots > 0)
        {
            foreach(GameObject slot in AllInventorySlots)
            {
                SlotManager temp = slot.GetComponent<SlotManager>();

                // if slot stack is empty
                if(temp.SlotStackIsEmpty)
                {
                    // add item into the new slot
                    temp.AddItemToSlotStack(item);
                    // minus away an empty slot
                    _emptySlots--;
                    return true;
                }
            }
        }

        return false;
    }

    // When item looted by player, trigger this
    public bool AddItemToSlot(ItemDatabase item)
    {
        foreach (GameObject slot in AllInventorySlots)
        {
            // get all the slots 
            SlotManager temp = slot.GetComponent<SlotManager>();

            // if the slot is occupied
            if (!temp.SlotStackIsEmpty)
            {
                // if the item in the slot is the same as looted
                if (temp.CurrentItemOnSlot.ItemName == item.ItemName)
                {
                    // and slot is not full yet
                    if (!temp.SlotNotStackable)
                    {
                        // add item into the slot
                        temp.AddItemToSlotStack(item);
                        return true;
                    }
                }
            }
            else // slot is empty
            {
                // put item in new empty slot
                PlaceIntoEmptySlot(item);
                return true;
            }
        }

        return false;
    }

    // Swap items slot
    public void MoveItem(GameObject clicked)
    {
        if(_slotMoveFrom == null)
        {
            if(!clicked.GetComponent<SlotManager>().SlotStackIsEmpty)
            {
                _slotMoveFrom = clicked.GetComponent<SlotManager>();
                _slotMoveFrom.GetComponent<Image>().color = Color.gray;
            }
        }
        else if(_slotMoveTo == null)
        {
            _slotMoveTo = clicked.GetComponent<SlotManager>();
        }

        if(_slotMoveTo != null && _slotMoveFrom != null)
        {
            Stack<ItemDatabase> tempTo = new Stack<ItemDatabase>(_slotMoveTo.SlotItemStack);
            _slotMoveTo.ReplaceAddItems(_slotMoveFrom.SlotItemStack);

            if (tempTo.Count == 0)
            {
                _slotMoveFrom.ClearSlot();
            }
            else
            {
                _slotMoveFrom.ReplaceAddItems(tempTo);
            }

            _slotMoveFrom.GetComponent<Image>().color = Color.white;
            _slotMoveTo = null;
            _slotMoveFrom = null;
        }
    }

    // Swapping of slots
    public void MoveSlot(GameObject clicked)
    {
        SelectSlotToMove(clicked);
        SwapSelectedSlot();

    }

    public void SelectSlotToMove(GameObject selectedSlot)
    {
        // when we first select a slot to move from; from is null at first
        if (_slotMoveFrom == null)
        {
            // and if the slot is not empty
            if (!selectedSlot.GetComponent<SlotManager>().SlotStackIsEmpty)
            {
                // from is set to the first selected slot
                _slotMoveFrom = selectedSlot.GetComponent<SlotManager>();
                // set the first selected slot to grey
                _slotMoveFrom.GetComponent<Image>().color = Color.gray;
                // set the icon to gray too
                _slotMoveFrom.transform.Find("Icon").GetComponentInChildren<Image>().color = Color.gray;
            }
        }
        else if (_slotMoveTo == null) // when we select the second slot to move to; to is null at first
        {
            // Set to the second selected slot
            _slotMoveTo = selectedSlot.GetComponent<SlotManager>();
        }
    }

    public void SwapSelectedSlot()
    {
        // When both to and from are set to the selected slots
        if (_slotMoveTo != null && _slotMoveFrom != null)
        {
            // Store the items in the TO slot in a temporary stack first
            Stack<ItemDatabase> tempTo = new Stack<ItemDatabase>(_slotMoveTo.SlotItemStack);
            // Replace and swap the items in TO slot into the FROM slot
            _slotMoveTo.ReplaceAddItems(_slotMoveFrom.SlotItemStack);

            // If the second selected, TO slot was empty 
            if (tempTo.Count == 0)
            {
                // Clear the FROM slot as nothing to put in the TO slot
                _slotMoveFrom.ClearSlot();
            }
            else // If the TO slot had items
            {
                // Add the items in TO slot to FROM slot
                _slotMoveFrom.ReplaceAddItems(tempTo);
            }

            // Then reset everything back to original
            _slotMoveFrom.GetComponent<Image>().color = Color.white;
            _slotMoveFrom.transform.Find("Icon").GetComponentInChildren<Image>().color = Color.white;
            _slotMoveTo = null;
            _slotMoveFrom = null;
        }
    }
}
