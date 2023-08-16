using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Add("snow-man", 1, 500, "snowman", ItemType.design);
        Add("icicle", 1, 300, "icicle", ItemType.design);
        Add("igloo", 1, 300, "igloo", ItemType.design);
        Add("stalags", 1, 300, "stalags", ItemType.design);
        Add("snow-logs", 1, 300, "snowlogs", ItemType.design);
        Add("snow-root", 1, 300, "snowroot", ItemType.design);
        Add("penguin", 1, 500, "penguin", ItemType.design);
        Add("sledge", 1, 300, "sledge", ItemType.design);
        Add("polar-bear", 1, 500, "polarbear", ItemType.design);
        Add("snow-dead-tree", 1, 300, "snowdeadtree", ItemType.design);
        Add("ice-tree", 1, 300, "icetree", ItemType.design);
    }

    void Add(string itemName, int itemValue, int itemPrice, string itemDesc, ItemType itemType)
    {
        items.Add(new Item(itemName, itemValue, itemPrice, itemDesc, itemType, Resources.Load<Sprite>("ItemImages/" + itemName)));
    }
}
