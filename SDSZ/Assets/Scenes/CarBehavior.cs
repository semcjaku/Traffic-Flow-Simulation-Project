using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    enum Direction {up=1, right, down, left};

    public float currentSpeed, xSpeedCompound, ySpeedCompound;
    //public int nextTurn;

    private Rigidbody2D rb2d;
    private CapsuleCollider2D carCollider;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        carCollider = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        SetMovementCompounds(); //set compounds
        Vector2 movement = new Vector2(rb2d.position.x+xSpeedCompound,rb2d.position.y+ySpeedCompound); //set movement vector
        rb2d.MovePosition(movement); //move the car
    }

    void CarStop()
    {
        currentSpeed = 0;
    }

    void CarSlowDown(float x)
    {
        currentSpeed-=x;
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
            CarSlowDown(0.04f);
    }*/

    void OnTriggerStay2D(Collider2D other) //#WIP #Temp
    {
        if (other.gameObject.tag == "Cross section") //Executed while car on crossroads
            rb2d.MoveRotation(90);
    }
}
