
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public int value;



    [Header("Only Gameplay")]
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);
    public TileBase tile;
    [Header("Only UI")]
    public bool stackable = true;
    [Header("Both")]
    public Sprite image; //sprite mostrada no inventário

}

public enum ItemType
{
    CraftingItems,
    Tool,
    Food
}

public enum ActionType
{
    Dig,
    Chop,
    Fight,
    Fishing,
    Planting
}
