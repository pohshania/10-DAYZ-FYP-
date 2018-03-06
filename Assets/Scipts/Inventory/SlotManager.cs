using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour, IPointerClickHandler
{
    [Header("Slot UI")]
    public Text StackText;

    private Stack<ItemDatabase> ItemStack;
    public Stack<ItemDatabase> SlotItemStack
    {
        get { return ItemStack; }
        set { ItemStack = value; }
    }

    void Start()
    {
        //instantiates the items stack
        ItemStack = new Stack<ItemDatabase>();
    }

    // get the current item on the slot
    public ItemDatabase CurrentItemOnSlot
    {
        get { return ItemStack.Peek(); }
    }

    // show if there is nothing inside the slot
    public bool SlotStackIsEmpty
    {
        get { return ItemStack.Count == 0; }
    }

    // show the slot stack is full
    public bool SlotNotStackable
    {
        get { return CurrentItemOnSlot.ItemMaxStack == ItemStack.Count; }
    }

    // Add item into slot stack
    public void AddItemToSlotStack(ItemDatabase item)
    {
        // Add item into stack
        ItemStack.Push(item);

        // when there is more than 1 item in same stack/slot
        if (ItemStack.Count >= 1)
        {
            // print out the stack count
            StackText.text = ItemStack.Count.ToString();
        }

        // Set the item icon 
        SetItemIcon(item);
    }

    // Remove item from the slot stack
    private void UseItem()
    {
        // if there is any item in the slot
        if(!SlotStackIsEmpty)
        {
            // take out the item
            ItemStack.Pop().UseItem();

            // when there is 1 or more item in stack/slot
            if(ItemStack.Count >= 1)
            {
                // print out the stack count
                StackText.text = ItemStack.Count.ToString();
            }
            else
            {
                // else make the text empty
                StackText.text = string.Empty;
            }

            // when we remove the item, the slot becomes empty
            if(SlotStackIsEmpty)
            {
                // change the icon back to empty slot sprite
                RemoveItemIcon();
                // add another new empty slot
                InventoryManager.EmptySlots++;
            }
        }
    }

    // Set the item icon when looted into an empty slot
    private void SetItemIcon(ItemDatabase item)
    {
        this.transform.Find("Icon").gameObject.active = true;
        this.transform.Find("Icon").GetComponentInChildren<Image>().enabled = true;
        this.transform.Find("Icon").GetComponentInChildren<Image>().sprite = item.ItemSprite;
    }

    // Remove the item icon when slot becomes empty
    private void RemoveItemIcon()
    {
        this.transform.Find("Icon").GetComponentInChildren<Image>().sprite = null;
        this.transform.Find("Icon").GetComponentInChildren<Image>().enabled = false;
        this.transform.Find("Icon").gameObject.active = false;
    }

    // Use this when replace occupied stack with another stack
    public void ReplaceAddItems(Stack<ItemDatabase> items)
    {
        // instantiate a new ItemStack
        this.ItemStack = new Stack<ItemDatabase>(items);

        // when there is 1 or more item in stack/slot
        if (ItemStack.Count >= 1)
        {
            // print out the stack count
            StackText.text = ItemStack.Count.ToString();
        }
        else
        {
            // else make the text empty
            StackText.text = string.Empty;
        }

        // Change icon to current item
        SetItemIcon(CurrentItemOnSlot);
    }

    // Clear everything in the slot
    public void ClearSlot()
    {
        // Remove item stack
        ItemStack.Clear();
        // Remove icon
        RemoveItemIcon();
        // Remove stack text
        StackText.text = string.Empty;
    }

    // When the slot is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }
}
