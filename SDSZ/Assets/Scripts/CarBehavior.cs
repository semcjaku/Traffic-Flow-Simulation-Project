using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    enum Direction {up=1, right, down, left};

    public float currentSpeed, currentAcceleration, xSpeedCompound, ySpeedCompound, turnRadius;
    public int nextTurn;
    public const float maxSpeed = 0.05f;

    private bool stayBlocade_fl, canAccelerate_fl;
    private float stayBlocadeTimer_tim, stayTime_tim, stayTime1, stayTime2;
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
        /////////////////TEMP///////////////////////////
        nextTurn = (int)Direction.right;
        currentAcceleration = 0.0005f;
        ////////////////////////////////////
        stayBlocade_fl = false;
        canAccelerate_fl = true;
    }

    void FixedUpdate()
    {
        Accelerate(currentAcceleration);
        SetMovementCompounds(); //set compounds
        Vector2 movement = new Vector2(rb2d.position.x+xSpeedCompound,rb2d.position.y+ySpeedCompound); //set movement vector
        rb2d.MovePosition(movement); //move the car
        stayBlocadeTimer_tim -= Time.deltaTime;
    }

    void CarStop()
    {
        canAccelerate_fl = false;
        currentSpeed = 0;
    }

    void CarStart()
    {
        canAccelerate_fl = true;
        currentSpeed = 0.025f;
    }

    void CarSlowDown(float x)
    {
        currentSpeed-=x;
    }

    void Accelerate(float a)
    {
        if (currentSpeed < maxSpeed && canAccelerate_fl)
            currentSpeed += a;
        if (currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;
    }

    void RoundRotation()
    {
        if(Math.Abs(rb2d.rotation) % 360 >= 340f || Math.Abs(rb2d.rotation) % 360 <= 20f)
        {
            rb2d.MoveRotation(0);
        }
        else if((Math.Abs(rb2d.rotation) % 360 >= 70f && Math.Abs(rb2d.rotation) % 360 <= 110f && rb2d.rotation > 0) || (Math.Abs(rb2d.rotation) % 360 >= 250f && Math.Abs(rb2d.rotation) % 360 <= 290f && rb2d.rotation < 0))
        {
            rb2d.MoveRotation(90);
        }
        else if (Math.Abs(rb2d.rotation) % 360 >= 160f && Math.Abs(rb2d.rotation) % 360 <= 200f)
        {
            rb2d.MoveRotation(180);
        }
        else if ((Math.Abs(rb2d.rotation) % 360 >= 70f && Math.Abs(rb2d.rotation) % 360 <= 110f && rb2d.rotation < 0) || (Math.Abs(rb2d.rotation) % 360 >= 250f && Math.Abs(rb2d.rotation) % 360 <= 290f && rb2d.rotation > 0))
        {
            rb2d.MoveRotation(270);
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

    void OnTriggerEnter2D(Collider2D other) //#WIP #Temp
    {
        if (other.gameObject.tag == "Cross section") //Executed while car enters crossroads
        {
            canAccelerate_fl = false;
            if (!stayBlocade_fl)
            {
                stayTime_tim = 0f;
                stayTime1 = (carCollider.size.y / 2f) / (currentSpeed*50f);  //Time in which half of the car gets beyond the box border
                switch (nextTurn)
                {
                    case (int)Direction.left:
                        turnRadius = crossroads.radiusLeft;
                        break;
                    case (int)Direction.right:
                        turnRadius = crossroads.radiusRight*(-1f);
                        break;
                }
                stayTime2 = ((carCollider.size.y + (float)Math.PI * Math.Abs(turnRadius)) / 2f) / (currentSpeed*50f);  //Time in which the car makes the turn and its half gets beyond other border
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) //#WIP #Temp
    {
        if (other.gameObject.tag == "Cross section") //Executed while car on crossroads
        {
            if(!stayBlocade_fl)
            {
                if (stayTime_tim >= stayTime1 && stayTime_tim < stayTime2 && (nextTurn==(int)Direction.right || nextTurn==(int)Direction.left))
                {
                    float k = 1f;
                    if (nextTurn == (int)Direction.right)
                        k = -1f;
                    rb2d.MoveRotation(rb2d.rotation + k * (90f / ((stayTime2 - stayTime1) / 0.02f))); //wzor na zmiane obrotu w czasie zalezny od predkosci z uwzglednieniem odswiezania co 0.02s
                }
                stayTime_tim += Time.deltaTime;
            }
            
        }  
    }

    void OnTriggerExit2D(Collider2D other) //#WIP #Temp
    {
        if (other.gameObject.tag == "Cross section") //Executed while car enters crossroads
        {
            canAccelerate_fl = true;
            stayBlocade_fl = true;
            stayBlocadeTimer_tim = 0.1f;
            RoundRotation();
        }
    }
}
