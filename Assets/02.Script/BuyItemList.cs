using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemList : MonoBehaviour
{
    public static BuyItemList instance;
    public List<Item> items = new List<Item>();
    public static List<string> buyCharacterList = new List<string>();
    public static string pickCh = "BEAR"; 

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Add("snow-man", 1, 500, "snowman", ItemType.design);
        Add("igloo", 1, 300, "igloo", ItemType.design);
        Add("penguin", 1, 300, "penguin", ItemType.design);
        buyCharacterList.Add("BEAR");
    }

    void Add(string itemName, int itemValue, int itemPrice, string itemDesc, ItemType itemType)
    {
        items.Add(new Item(itemName, itemValue, itemPrice, itemDesc, itemType, Resources.Load<Sprite>("ItemImages/" + itemName)));
    }
    
}
