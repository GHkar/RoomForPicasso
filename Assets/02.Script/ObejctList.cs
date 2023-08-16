using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObejctList : MonoBehaviour
{
    public static ObejctList instance;
    public Transform slot;
    public List<Slots> slotScripts = new List<Slots>();
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Invoke("AddItem",0.01f);
        for (int i = 0; i < 5; i++)
        {
            Transform newSlot = Instantiate(slot);
            newSlot.name = "Slot2";
            newSlot.parent = transform;
            newSlot.localScale = new Vector3(1.027538f, 2.369516f, Vector3.one.z);
            newSlot.localPosition = new Vector3(Vector3.one.x, Vector3.one.y, Vector3.one.z - 1);


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
        }
    }
    void AddItem()
    {
        for (int i = 0; i < 5; i++)
        {
            slotScripts[i].item = BuyItemList.instance.items[i];
            ItemImageChange(slotScripts[i].transform);
        }
    }
}
