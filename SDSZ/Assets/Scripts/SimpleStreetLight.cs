using System.Collections;
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
			if (this.gameObject.GetComponent<SpriteRenderer>().sprite == RedLight)
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = YellowLight;
                status = (int)LightColor.yellow;
                timer = yellowtogreen;
				return;
			}
			if (this.gameObject.GetComponent<SpriteRenderer>().sprite == YellowLight)
			{
				if (WasGreen == false)
				{
					this.gameObject.GetComponent<SpriteRenderer>().sprite = GreenLight;
                    status = (int)LightColor.green;
                    timer = lightphase;
					WasGreen = true;
					return;
				}
				else
				{
					this.gameObject.GetComponent<SpriteRenderer>().sprite = RedLight;
                    status = (int)LightColor.red;
                    timer = lightphase; 
					return;
				}
			}
			if (this.gameObject.GetComponent<SpriteRenderer>().sprite == GreenLight)
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = YellowLight;
                status = (int)LightColor.yellow;
                timer = yellowtogreen; //normalnie greentoyellow ale to taka sama wartość
				return;
			}
		}
	}
}