using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignButton : MonoBehaviour
{
    //리스트 생성 후
    public Transform slot;
    public Transform backGround;

    int page = 1;

    public void LeftArrowClick()
    {
        if (page == 2)
        {
            page = 1;
            for(int i = 0; i < 6; i++)
            {
                backGround.GetChild(0).GetChild(i).GetComponent<Slots>().item = ItemDatabase.instance.items[i];
                ItemImageChange(backGround.GetChild(0).GetChild(i));
            }
        }
    }
    public void RightArrowClick()
    {
        if (page == 1)
        {
            page = 2;
            for (int i = 0; i < 6; i++)
            {
                if (i < 5)
                {
                    backGround.GetChild(0).GetChild(i).GetComponent<Slots>().item = ItemDatabase.instance.items[i + 6];
                    ItemImageChange(backGround.GetChild(0).GetChild(i));
                }
                else
                {
                    Destroy(backGround.GetChild(0).GetChild(i).gameObject);

                    Transform newSlot = Instantiate(slot);
                    newSlot.name = "Slot";
                    newSlot.parent = backGround.GetChild(0);
                    newSlot.localScale = Vector3.one;
                    newSlot.localPosition = new Vector3(Vector3.one.x, Vector3.one.y, Vector3.one.z - 1);
                }
            }
        }
    }
    
    public void ClickDesign()
    {
        Debug.Log(this.GetComponent<Slots>().item.itemName);
    }

    void ItemImageChange(Transform _slot)
    {
        if (_slot.GetComponent<Slots>().item.itemValue == 0)
            _slot.GetChild(0).gameObject.SetActive(false);
        else
        {
            _slot.GetChild(0).gameObject.SetActive(true);
            _slot.GetChild(0).GetComponent<Image>().sprite = _slot.GetComponent<Slots>().item.itemImage;
            _slot.GetChild(1).GetComponent<Text>().text = _slot.GetComponent<Slots>().item.itemName;
        }
    }
}
