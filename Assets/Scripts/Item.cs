using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Data/Item")]
public class Item : ScriptableObject
{
    [Serializable]
    public struct ItemInfo
    { 
        public int id;
        public ItemType itemType;
        public string itemName;
        public Sprite itemImage;
        public ActionType actionType;
        public Vector2Int range = new Vector2Int(5, 4);
    }
}

public enum ItemType
{
    Equipment,
    Tool,
    Weapon,
    Block,
    Wall,
    Potion,
    None,
}

public enum ActionType
{
    Mine,
    Wall,
    Attack,
}