using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TowerData", menuName = "Data/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public int id;
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