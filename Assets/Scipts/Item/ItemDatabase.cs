using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemDatabase : ScriptableObject
{
    public enum ITEM_TYPE
    {
        HEALTH = 0,
        HUNGER,
        THIRST
    }

    public Sprite ItemSprite;
    public string ItemName;
    public ITEM_TYPE ItemType;
    public string ItemDescription;
    public int ItemMaxStack;

    public void UseItem()
    {
        switch (ItemType)
        {
            case ItemDatabase.ITEM_TYPE.HEALTH:
                {
                    Debug.Log("Used a health item");
                    break;
                }
            case ItemDatabase.ITEM_TYPE.HUNGER:
                {
                    Debug.Log("Used a hunger item");
                    break;
                }
            case ItemDatabase.ITEM_TYPE.THIRST:
                {
                    Debug.Log("Used a thirst item");
                    break;
                }
        }
    }
}
