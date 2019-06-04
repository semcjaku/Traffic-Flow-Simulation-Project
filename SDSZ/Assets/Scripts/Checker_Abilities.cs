using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checker_Abilities : MonoBehaviour
{
    Slider car_slider;
    
    void Start()
    {
        car_slider = GameObject.Find("Car_Amount_slider").GetComponent<Slider>();
        car_slider.transform.localPosition = new Vector3(-360, -245, 0);
        car_slider.transform.localScale = new Vector3(0.8f, 0.8f, 0);
        //car_slider.Value.disable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
