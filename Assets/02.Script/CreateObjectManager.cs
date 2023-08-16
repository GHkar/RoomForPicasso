using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectManager : MonoBehaviour
{
    public Camera cm;
    public Transform control_tr;
    public void click(string ob_name)
    {
        //GameObject ob = (GameObject)Instantiate(Resources.Load("Prefabs/" + ob_name));
        //sob.name = ob_name;
        //ob.transform.position = new Vector3(cm.WorldToScreenPoint.x, cm.pixelHeight /2, cm.gameObject.layer + 3.0f));


        GameObject ob = (GameObject)Instantiate(Resources.Load("Prefabs/" + "penguin"));
    }
}
