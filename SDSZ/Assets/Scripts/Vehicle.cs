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

    private bool stayBlocade_fl;
    private float stayBlocadeTimer_tim, stayTime_tim, stayTime1, stayTime2;
    private Rigidbody2D rb2d;
    private CapsuleCollider2D carCollider;
    private Crossroads crossroads;
    private SimpleStreetLight light01;


}
