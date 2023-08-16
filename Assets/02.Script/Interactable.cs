using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Animmal.Animmals1; // 캐릭터 표시
using System;

public class Interactable : MonoBehaviour
{
    public MeshRenderer renderer;
    private string o_name;
    public Text input;
    public Text point;
    

    public Transform slot;
    public Transform backGround;

    Vector3 initialPos;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        o_name = renderer.name;
        input = GameObject.Find("selectText").GetComponent<Text>();
        point = GameObject.Find("point").GetComponent<Text>();
        backGround = GameObject.Find("backgroud").GetComponent<Transform>();
        //previewmanager = GameObject.Find("PreviewManager").GetComponent<GameObject>();

    }

    public void Pressed()        //컨트롤러 입력값
    {
        switch(o_name)
        {
            //디자인씬
            case "LArrow" :
                if (SlotList.page == 2)
                {
                    SlotList.page = 1;
                    for (int i = 0; i < 6; i++)
                    {
                        backGround.GetChild(0).GetChild(i).GetComponent<Slots>().item = ItemDatabase.instance.items[i];
                        ItemImageChange(backGround.GetChild(0).GetChild(i));
                    }
                }
                break;

            case "RArrow" :
                if (SlotList.page == 1)
                {
                    SlotList.page = 2;
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
                break;

            case "Slot" :
                input.text = renderer.transform.GetComponent<Slots>().item.itemName;
                SlotList.price = renderer.transform.GetComponent<Slots>().item.itemPrice;
                break;

            case "PayButton" :
                if (Convert.ToInt32(point.text) > 0)
                {
                    for (int i = 0; i < ItemDatabase.instance.items.Count; i++)
                    {
                        if (input.text == ItemDatabase.instance.items[i].itemName)
                        {
                            if (ItemDatabase.instance.items[i].itemValue > 0)
                            {
                                ItemDatabase.instance.items[i].itemValue = 0;
                                point.text = (Convert.ToInt32(point.text) - SlotList.price).ToString();
                            }
                        }
                    }
                }
                break;
            //백 버튼 공통
            case "BackButton" :
                SceneManager.LoadScene("main");
                break;
            //메인 씬
            case "character" :
                SceneManager.LoadScene("animalScene");
                break;
            case "design" :
                SceneManager.LoadScene("DesignScene");
                break;
            //캐릭터 씬
            case "RArrow-ch" :
                GameObject.Find("PreviewManager").GetComponent<PreviewManager>().RightClicked();
                break;
            case "LArrow-ch" :
                GameObject.Find("PreviewManager").GetComponent<PreviewManager>().LeftClicked();
                break;
            case "BuyButton" :
                GameObject.Find("BuyController").GetComponent<BuyController>().BuybtnCh();
                break;
            case "PickButton" :
                GameObject.Find("BuyController").GetComponent<BuyController>().Pickbtn();
                break;
            //오브젝트 생성
            case "Slot2" :
                string itemname = renderer.transform.GetComponent<Slots>().item.itemName;
                GameObject.Find("CreateObjectManager").GetComponent<CreateObjectManager>().click(itemname);
                break;
            default:
                break;
        }
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
}
