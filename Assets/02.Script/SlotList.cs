using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SlotList : MonoBehaviour
{
    public static SlotList instance;
    public static int page = 1;
    public static int price = 0;
    public Transform slot;
    public List<Slots> slotScripts = new List<Slots>();
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for(int i = 0; i < 6; i++)
        {
            Transform newSlot = Instantiate(slot);
            newSlot.name = "Slot";
            newSlot.parent = transform;
            newSlot.localScale = Vector3.one;
            newSlot.localPosition = new Vector3(Vector3.one.x, Vector3.one.y, Vector3.one.z -1);


            slotScripts.Add(newSlot.GetComponent<Slots>());
        }
        AddItem();
    }

    void ItemImageChange(Transform _slot)
    {
        if (_slot.GetComponent<Slots>().item.itemValue == 0)
            _slot.GetChild(0).gameObject.SetActive(false);
        else
        {
            _slot.GetChild(0).gameObject.SetActive(true);
            _slot.GetChild(0).GetComponent<Image>().sprite = _slot.GetComponent<Slots>().item.itemImage;
            _slot.GetChild(1).GetComponent<Text>().text = _slot.GetComponent<Slots>().item.itemName + " (" +
                _slot.GetComponent<Slots>().item.itemPrice.ToString() + ")";
        }
    }
    void AddItem()
    {
        for (int i = 0; i < 6; i++)
        {
            if (slotScripts[i].item.itemValue == 0)
            {
                slotScripts[i].item = ItemDatabase.instance.items[i];
                ItemImageChange(slotScripts[i].transform);
            }
        }
    }
}
