using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Item Data", menuName = "Data/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public int id;
    public string itemName;
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
}

public enum ItemType
{
    None,
    Equipment,
    Tool,
    Weapon,
    Block,
    Wall,
    Potion,
    Torch,
}

public enum ActionType
{
    None,
    Mine,
    CutDown,
    WallCrush,
    Broadsword,
    Shortsword,
}