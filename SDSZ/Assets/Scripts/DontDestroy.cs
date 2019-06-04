using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //Slider myslider = GameObject.Find("Car_Amount_slider").GetComponent<Slider>();

        
        DontDestroyOnLoad(this.gameObject);

        
        
    }

    
}
