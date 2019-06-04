using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderValue : MonoBehaviour
{
    public Text sliderText;
    public Slider slider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderText.text = slider.value.ToString();
    }
}
