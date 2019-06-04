using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checker_Abilities : MonoBehaviour
{
    Slider car_slider;
    GameObject[] components;
    GameObject value_of_cars;
    void Start()
    {
        car_slider = GameObject.Find("Car_Amount_slider").GetComponent<Slider>();
        car_slider.transform.localPosition = new Vector3(-800, -500, 0);
        car_slider.transform.localScale = new Vector3(0.8f, 0.8f, 0);
        components = GameObject.FindGameObjectsWithTag("slider_component");
        value_of_cars = GameObject.FindWithTag("slider_value");
        //car_slider.Value.disable;
        foreach (GameObject comp in components)
        {
            comp.SetActive(false);
        }
        value_of_cars.transform.position += new Vector3(+90, +38, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
