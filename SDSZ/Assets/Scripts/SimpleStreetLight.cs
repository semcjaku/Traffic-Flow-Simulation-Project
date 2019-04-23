﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStreetLight : MonoBehaviour
{
    public enum LightColor {red,yellow,green};

	public Sprite RedLight;
	public Sprite YellowLight;
	public Sprite GreenLight;
    public int status;


	float redtoyellow = 2f;
	float yellowtogreen = 1f;
	float lightphase = 10f;

	public float timer = 2.5f;

    private GameObject carCrossing=null;

	bool WasGreen = false;
	// Start is called before the first frame update
	void Start()
	{
		this.gameObject.GetComponent<SpriteRenderer>().sprite = RedLight;
        status = 0;
	}

	// Update is called once per frame
	void Update()
	{
        timer -= Time.deltaTime;
		if (timer <= 0)
		{
			if (this.gameObject.GetComponent<SpriteRenderer>().sprite == RedLight) //R->Y
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = YellowLight;
                status = (int)LightColor.yellow;
                timer = yellowtogreen;
				return;
			}
			if (this.gameObject.GetComponent<SpriteRenderer>().sprite == YellowLight) //Y->
			{
				if (WasGreen == false)                                                  //->G
				{
					this.gameObject.GetComponent<SpriteRenderer>().sprite = GreenLight;
                    status = (int)LightColor.green;
                    if (carCrossing != null) // && !carCrossing.Equals(null) jeśli byłyby błędy
                        carCrossing.SendMessage("CarStart");
                    timer = lightphase;
					WasGreen = true;
					return;
				}
				else                                                                    //->R
				{
					this.gameObject.GetComponent<SpriteRenderer>().sprite = RedLight;
                    status = (int)LightColor.red;
                    timer = lightphase; 
					return;
				}
			}
			if (this.gameObject.GetComponent<SpriteRenderer>().sprite == GreenLight) //G->Y
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = YellowLight;
                status = (int)LightColor.yellow;
                timer = yellowtogreen; //normalnie greentoyellow ale to taka sama wartość
				return;
			}
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            carCrossing = other.gameObject;
            if (status == (int)LightColor.red)
                carCrossing.SendMessage("CarStop");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            carCrossing = null;
        }
    }
}