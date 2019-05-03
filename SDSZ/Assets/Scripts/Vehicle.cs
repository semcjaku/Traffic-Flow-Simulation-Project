using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vehicle", menuName = "Vehicle/Cars")]
public class Vehicle : ScriptableObject
{
    // Start is called before the first frame update
    enum Direction { up = 1, right, down, left };

    public float currentSpeed, xSpeedCompound, ySpeedCompound, turnRadius;
    public int nextTurn;

    public Sprite artwork;

    public bool stayBlocade_fl;
    public float stayBlocadeTimer_tim, stayTime_tim, stayTime1, stayTime2;
    public Rigidbody2D rb2d;
    public CapsuleCollider2D carCollider;
    public Crossroads crossroads;
    public SimpleStreetLight light01;


}
