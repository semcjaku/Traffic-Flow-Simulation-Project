using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleBehaviour : MonoBehaviour
{
    public Vehicle vehicle;
    

    enum Direction { up = 1, right, down, left };

    public float localcurrentSpeed;
    public float localxSpeedCompound;
    public float localySpeedCompound;
    public float localturnRadius;
    public float localcurrentAcceleration;

    public const float localmaxSpeed = 0.05f;

    public int nextTurn;

    Image m_Image;
    public Sprite m_Sprite;

    public bool localstayBlocade_fl;
    public bool localcanAccelerate_fl;
    public bool SeenOtherCar_fl;
    public bool DisabledEdge_fl;

    private float stayBlocadeTimer_tim, stayTime_tim, stayTime1, stayTime2;
    private Rigidbody2D rb2d;
    private CapsuleCollider2D carCollider;
    private Crossroads crossroads;
    private BoxCollider2D VirtualBumper;

    private Collider2D another_car_collider;
    public VehicleSpawner daddy;
    private Rigidbody2D another_car_rb2d;


    public Vector2 offset_vector;
    //private SimpleStreetLight light01;

    void Start()
    {

        m_Image = GetComponent<Image>();

        localcurrentSpeed = vehicle.currentSpeed;
        localxSpeedCompound = vehicle.xSpeedCompound;
        localcurrentAcceleration = vehicle.currentAcceleration;


        rb2d = GetComponent<Rigidbody2D>();
        carCollider = GetComponent<CapsuleCollider2D>();
        VirtualBumper = GetComponent<BoxCollider2D>();

        daddy = GameObject.Find("CarSpawner").GetComponent<VehicleSpawner>();
        crossroads = GameObject.Find("Obszar skrzyzowania").GetComponent<Crossroads>();
        //light01 = GameObject.Find("TrafficLight01").GetComponent<SimpleStreetLight>();
        /////////////////TEMP///////////////////////////
        nextTurn = 2;//UnityEngine.Random.Range(1,5);
        localcurrentAcceleration = 0.0005f;
        ////////////////////////////////////
        localstayBlocade_fl = vehicle.stayBlocade_fl;
        localcanAccelerate_fl = vehicle.canAccelerate_fl;
    }

    void FixedUpdate()
    {
        Accelerate(localcurrentAcceleration);
        SetMovementCompounds(); //set compounds
        Vector2 movement = new Vector2(rb2d.position.x + localxSpeedCompound, rb2d.position.y + localySpeedCompound); //set movement vector
        rb2d.MovePosition(movement); //move the car
        stayBlocadeTimer_tim -= Time.deltaTime;
        if (stayBlocadeTimer_tim <= 0)
            localstayBlocade_fl = false;
        //m_Image.sprite = m_Sprite;
        NextCarChecking();
    }

    void NextCarChecking()
    {
        foreach (GameObject another_one in daddy.existing_cars)
        {
            if(another_one != this.gameObject)
            {
                another_car_collider = another_one.GetComponent<CapsuleCollider2D>();
                another_car_rb2d = another_one.GetComponent<Rigidbody2D>();
                float line_of_sight = (localySpeedCompound/localxSpeedCompound) * (another_car_rb2d.position.x - rb2d.position.x) + rb2d.position.y;
                if (another_car_rb2d.position.y >= line_of_sight-0.5f*carCollider.size.y && another_car_rb2d.position.y <= line_of_sight + 0.5f * carCollider.size.y) //if car is in line of sight
                {
                    //vvvvvvvvvvvvvvvvvvvvvTEN IF NIE BĘDZIE DZIAŁAŁ PRZEZ BŁĘDY NUMERYCZNE -> TRZEBA ZWIĘKSZYĆ TOLERANCJĘ WARUNKÓW GRANICZNYCH -> TO MOŻE RODZIĆ BŁĘDY W DRUGĄ STRONĘ => DO PRZEMYŚLENIAvvvvvvvvvvvvvvvvvvvvvvvv
                    if ((((Math.Abs(rb2d.rotation) % 360 > 0f && Math.Abs(rb2d.rotation) % 360 < 180f && rb2d.rotation > 0) || (Math.Abs(rb2d.rotation) % 360 > 180f && rb2d.rotation < 0)) && another_car_rb2d.position.x <= rb2d.position.x) || //if seen car is actually in front of this car
                        (((Math.Abs(rb2d.rotation) % 360 > 180f && rb2d.rotation > 0) || (Math.Abs(rb2d.rotation) % 360 > 0f && Math.Abs(rb2d.rotation) % 360 < 180f && rb2d.rotation < 0)) && another_car_rb2d.position.x >= rb2d.position.x) ||
                        (Math.Abs(rb2d.rotation) % 360 == 180f && another_car_rb2d.position.y <= rb2d.position.y) ||
                        (Math.Abs(rb2d.rotation) % 360 == 0f && another_car_rb2d.position.y >= rb2d.position.y))
                    {
                        Debug.Log("Jestem: " + this + "Widzę auto: " + another_one);
                    }
                }
            }

        }
    }

    
    void CarStop()
    {
        localcanAccelerate_fl = false;
        localcurrentSpeed = 0;
    }

    void CarStart()
    {
        localcanAccelerate_fl = true;
        localcurrentSpeed = 0.025f;
    }

    void CarSlowDown(float x)
    {
        if (localcurrentSpeed > x)
            localcurrentSpeed -= x;
        else
            localcurrentSpeed = 0;

    }

    void Accelerate(float a)
    {
        if (localcurrentSpeed < localmaxSpeed && localcanAccelerate_fl)
            localcurrentSpeed += a;
        if (localcurrentSpeed > localmaxSpeed)
            localcurrentSpeed = localmaxSpeed;
    }

    void RoundRotation()
    {
        if (Math.Abs(rb2d.rotation) % 360 >= 340f || Math.Abs(rb2d.rotation) % 360 <= 20f)
        {
            rb2d.MoveRotation(0);
        }
        else if ((Math.Abs(rb2d.rotation) % 360 >= 70f && Math.Abs(rb2d.rotation) % 360 <= 110f && rb2d.rotation > 0) || (Math.Abs(rb2d.rotation) % 360 >= 250f && Math.Abs(rb2d.rotation) % 360 <= 290f && rb2d.rotation < 0))
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
        localxSpeedCompound = localcurrentSpeed * (float)Math.Sin((rb2d.rotation) / 360 * 2 * Math.PI);
        localySpeedCompound = localcurrentSpeed * (float)Math.Cos((rb2d.rotation) / 360 * 2 * Math.PI);
        localxSpeedCompound *= -1;                                   //Sign modifiers (different than in classical trigonometry because the angle of rotation in unity is 90 degrees smaller than in euclidean geometry)
        if (rb2d.rotation >= 180 && rb2d.rotation <= 270)
            localySpeedCompound *= -1;
    }

    void OnTriggerEnter2D(Collider2D other) //#WIP #Temp
    {
        

        if (other.gameObject.tag == "Cross section") //Executed while car enters crossroads
        {
            localcanAccelerate_fl = false;
            if (!localstayBlocade_fl)
            {
                stayTime_tim = 0f;
                stayTime1 = (carCollider.size.y / 2f) / (localcurrentSpeed * 50f);  //Time in which half of the car gets beyond the box border
                switch (nextTurn)
                {
                    case (int)Direction.left:
                        localturnRadius = crossroads.radiusLeft;
                        break;
                    case (int)Direction.right:
                        localturnRadius = crossroads.radiusRight * (-1f);
                        break;
                }
                stayTime2 = ((carCollider.size.y + (float)Math.PI * Math.Abs(localturnRadius)) / 2f) / (localcurrentSpeed * 50f);  //Time in which the car makes the turn and its half gets beyond other border
            }
            
        }
        /*if (VirtualBumper.IsTouching(other) && other.gameObject.tag == "Vehicle")
        {
            CarSlowDown(0.01f);
            SeenOtherCar_fl = true;
            Debug.Log("Spotkałem auto: " + gameObject);

        }*/
    }

    void OnTriggerStay2D(Collider2D other) //#WIP #Temp
    {
        if (other.gameObject.tag == "Cross section") //Executed while car on crossroads
        {
            if (!localstayBlocade_fl)
            {
                if (stayTime_tim >= stayTime1 && stayTime_tim < stayTime2 && (nextTurn == (int)Direction.right || nextTurn == (int)Direction.left))
                {
                    float k = 1f;
                    if (nextTurn == (int)Direction.right)
                        k = -1f;
                    rb2d.MoveRotation(rb2d.rotation + k * (90f / ((stayTime2 - stayTime1) / 0.02f))); //wzor na zmiane obrotu w czasie zalezny od predkosci z uwzglednieniem odswiezania co 0.02s
                }
                stayTime_tim += Time.deltaTime;
            }

        }
        

        /*if (other.gameObject.tag == "Vehicle")
        {
            CarSlowDown(0.01f);
            SeenOtherCar_fl = true;
            Debug.Log("Spotkałem auto: " + gameObject);
        }*/


    }

    

    void OnTriggerExit2D(Collider2D other) //#WIP #Temp
    {
        
            
        if (other.gameObject.tag == "Cross section") //Executed while car enters crossroads
        {
            localcanAccelerate_fl = true;
            localstayBlocade_fl = true;
            stayBlocadeTimer_tim = 0.1f;
            RoundRotation();
            Debug.Log("Wyszedłem!!!");
        }
    }


 
    //DEBUG-----------------------------------------------------------------------
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        Gizmos.DrawLine(rb2d.position, new Vector2(localxSpeedCompound/localcurrentSpeed+rb2d.position.x,localySpeedCompound/localcurrentSpeed + rb2d.position.y));
    }
}