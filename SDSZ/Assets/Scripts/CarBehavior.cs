using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    enum Direction {up=1, right, down, left};

    public float currentSpeed, xSpeedCompound, ySpeedCompound, turnRadius;
    public int nextTurn;

    private bool stayBlocade_fl;
    private float stayTimer_tim;
    private Rigidbody2D rb2d;
    private CapsuleCollider2D carCollider;
    private Crossroads crossroads;
    private SimpleStreetLight light01;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        carCollider = GetComponent<CapsuleCollider2D>();
        crossroads = GameObject.Find("Obszar skrzyzowania").GetComponent<Crossroads>();
        light01 = GameObject.Find("TrafficLight01").GetComponent<SimpleStreetLight>();
        nextTurn = (int)Direction.right;
        stayBlocade_fl = false;
    }

    void FixedUpdate()
    {
        SetMovementCompounds(); //set compounds
        Vector2 movement = new Vector2(rb2d.position.x+xSpeedCompound,rb2d.position.y+ySpeedCompound); //set movement vector
        rb2d.MovePosition(movement); //move the car
        stayTimer_tim -= Time.deltaTime;
    }

    void CarStop()
    {
        currentSpeed = 0;
    }

    void CarSlowDown(float x)
    {
        currentSpeed-=x;
    }

    void RoundRotation()
    {
        if(Math.Abs(rb2d.rotation) % 360 >= 340f || Math.Abs(rb2d.rotation) % 360 <= 20f)
        {
            rb2d.MoveRotation(0);
        }
        else if(Math.Abs(rb2d.rotation) % 360 >= 70f && Math.Abs(rb2d.rotation) % 360 <= 110f)
        {
            rb2d.MoveRotation(90);
        }
        else if (Math.Abs(rb2d.rotation) % 360 >= 160f && Math.Abs(rb2d.rotation) % 360 <= 200f)
        {
            rb2d.MoveRotation(180);
        }
        else if (Math.Abs(rb2d.rotation) % 360 >= 250f && Math.Abs(rb2d.rotation) % 360 <= 290f)
        {
            rb2d.MoveRotation(0);
        }
    }

    void SetMovementCompounds() //Sets the speed compounds based on the direction the car is headed, therefore making it go forward
    {
        xSpeedCompound = currentSpeed * (float)Math.Sin((rb2d.rotation) / 360 * 2 * Math.PI);
        ySpeedCompound = currentSpeed * (float)Math.Cos((rb2d.rotation) / 360 * 2 * Math.PI);
        xSpeedCompound *= -1;                                   //Sign modifiers (different than in classical trigonometry because the angle of rotation in unity is 90 degrees smaller than in euclidean geometry)
        if (rb2d.rotation >= 180 && rb2d.rotation <= 270)
            ySpeedCompound *= -1;
    }

    /*void OnTriggerEnter2D(Collider2D other) //#WIP #Temp
    {
        if (other.gameObject.tag == "Cross section") //Executed while car enters crossroads
        {
            if (!stayBlocade_fl)
            {
                if(light01.status == (int)SimpleStreetLight.LightColor.red)
                {
                    CarStop();
                }
                currentSpeed = 0.05f;
            }
        }
    }*/

    void OnTriggerStay2D(Collider2D other) //#WIP #Temp
    {
        if (other.gameObject.tag == "Cross section") //Executed while car on crossroads
        {
            if(!stayBlocade_fl)
            {
                switch (nextTurn)
                {
                    case (int)Direction.left:
                        turnRadius = crossroads.radiusLeft + 0.16f; //dlugosc auta
                        rb2d.MoveRotation(rb2d.rotation + (50f * currentSpeed / turnRadius)); //wzor na zmiane obrotu w czasie zalezny od predkosci z uwzglednieniem odswiezania co 0.02s
                        break;
                    case (int)Direction.right:
                        turnRadius = crossroads.radiusRight + 0.16f;
                        rb2d.MoveRotation(rb2d.rotation - (50f * currentSpeed / turnRadius));
                        break;
                }
            }
            
        }  
    }

    void OnTriggerExit2D(Collider2D other) //#WIP #Temp
    {
        stayBlocade_fl = true;
        stayTimer_tim = 0.1f;
        if (other.gameObject.tag == "Cross section") //Executed while car enters crossroads
            RoundRotation();
    }
}
