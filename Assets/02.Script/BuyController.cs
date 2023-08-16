using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyController : MonoBehaviour
{
    [Header("PointPannel")]
    public string PointUrl;
    public string BuyUrl;
    public string PickUrl;
    public Text mytext;
    public Text AssetName;
    public Text AB;
    public GameObject selectedText;
    // Start is called before the first frame update
    void Start()
    {
        selectedText.SetActive(false);
        /*
        PointUrl = "210.125.31.157/Point.php";
        BuyUrl = "210.125.31.157/Buy.php";
        PickUrl = "210.125.31.157/Pick.php";
        PointUpdate();
        */
    }


    public void BuybtnCh()
    {
        if (Convert.ToInt32(mytext.text) > 0)
        {
            bool isBuy = false;
            for (int i = 0; i < BuyItemList.buyCharacterList.Count; i++)
            {
                if (AssetName.text == BuyItemList.buyCharacterList[i])
                {
                    isBuy = true;
                    break;
                }
            }
            if (!isBuy)
            {
                mytext.text = (Convert.ToInt32(mytext.text) - 500).ToString();
                LoadPoint.point = Convert.ToInt32(mytext.text);
                BuyItemList.buyCharacterList.Add(AssetName.text);
            }
        }
    }
    /*
    public void Buybtn()
    {
        for (int i = 0; i < 11; i++)
        {
            if (AssetName.text.Equals(ItemDatabase.instance.items[i].itemName))
            {
                mytext.text = (LoadPoint.instance.point - ItemDatabase.instance.items[i].itemPrice).ToString();
                BuyItemList.instance.buyCharacterList.Add(ItemDatabase.instance.items[i]);
                break;
            }
        }
        //StartCoroutine(Buy());
    }
    */
    public void Pickbtn()
    {
        for (int i = 0; i < BuyItemList.buyCharacterList.Count; i++)
        {
            if(AssetName.text == BuyItemList.buyCharacterList[i])
            {
                BuyItemList.pickCh = AssetName.text;
                pickupdate();
                break;
            }
        }
        //StartCoroutine(Pick());
    }

    void Update()
    {
        pickupdate();
        for (int i = 0; i < BuyItemList.buyCharacterList.Count; i++)
        {
            if (AssetName.text == BuyItemList.buyCharacterList[i])
            {
                AB.text = "Already Have";
                break;
            }
            else
            {
                AB.text = "500 Point";
            }
        }
    }

    public void pickupdate()
    {
        if (BuyItemList.pickCh.Equals(AssetName.text))
        {
            selectedText.SetActive(true);
        }
        else
        {
            selectedText.SetActive(false);
        }
    }
    /*
    public void PointUpdate()
    {
        StartCoroutine(GetPoint());
    }

    IEnumerator GetPoint()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_user", "test");
        form.AddField("Input_pass", "1234");

        WWW webRequest = new WWW(PointUrl, form);
        yield return webRequest;
        //Debug.Log(webRequest.text);
        mytext.GetComponent<Text>().text = webRequest.text;
    }
    IEnumerator Buy()
    {
        WWWForm form = new WWWForm();
        // form.AddField("Input_user", AssetName.text);
        form.AddField("Input_user", "test");
        form.AddField("Input_pass", "1234");
        form.AddField("Input_animal", AssetName.text);
        //Debug.Log(AssetName.text);
        WWW webRequest = new WWW(BuyUrl, form);
        yield return webRequest;

        if (webRequest.text.Contains("a"))
        {
            Debug.Log(webRequest.text);
        }
        else mytext.GetComponent<Text>().text = webRequest.text;


    }
    IEnumerator Pick()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_user", "test");
        form.AddField("Input_pass", "1234");
        form.AddField("Input_animal", AssetName.text);

        WWW webRequest = new WWW(PickUrl, form);
        yield return webRequest;

    }
    // Update is called once per frame
    void Update()
    {
        GetPoint();
        Buy();
    }
    */
}
