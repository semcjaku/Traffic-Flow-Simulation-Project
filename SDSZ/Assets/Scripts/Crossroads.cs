using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossroads : MonoBehaviour
{
    public static float totalWaitingTime;
    public float radiusLeft;
    public float radiusRight;
    private BoxCollider2D area;

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider2D>();
        radiusLeft = 0.75f * area.size.x;
        radiusRight = 0.25f * area.size.x;
        totalWaitingTime = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
