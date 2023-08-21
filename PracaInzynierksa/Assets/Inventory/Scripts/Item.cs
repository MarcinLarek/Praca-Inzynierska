using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public string itemname;
    public string description;
    public TileBase tile;
    public Sprite image;
    public ItemType type;
    public ActionType actionType;
    public int value;
    public int price;
}
public enum ItemType
{
    Loot,
    Medical,
    Weapon,
    Special
}
public enum ActionType
{
    Consumable,
    Equipable,
    Trash
}


