using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPoint : MonoBehaviour
{
    public static LoadPoint instance;
    public Text Point;
    public static int point;
    // Start is called before the first frame update
    void Start()
    {
        point = 2000;
        Point.text = point.ToString();
    }
    void Update()
    {
        
    }
    
}
