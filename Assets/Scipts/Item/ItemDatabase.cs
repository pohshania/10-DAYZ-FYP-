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
}
