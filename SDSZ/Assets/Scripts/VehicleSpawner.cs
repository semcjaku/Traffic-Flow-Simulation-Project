using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject vehicle_car_orange;
    public GameObject vehicle_car_red;
    public GameObject vehicle_car_blue;

    public float spawnRate = 4.0f;

    float nextSpawn = 4.0f;

    float XshiftLeft = -6.35f;
    float YshiftLeft = -0.39f;

    float XshiftRight = +6.35f;
    float YshiftRight = +0.39f;

    float XshiftTop = -0.39f;
    float YshiftTop = +3.6f;

    float XshiftDown = +0.39f;
    float YshiftDown = -3.6f;

    Vector2 wheretoplace_top;
    Vector2 wheretoplace_down;
    Vector2 wheretoplace_left;
    Vector2 wheretoplace_right;

    int NumberOfVehicles = 6;
    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > nextSpawn && i<NumberOfVehicles)
            {
                if (i % 4 == 1)
                {
                    wheretoplace_left = new Vector2(XshiftLeft, YshiftLeft);
                    int colour_of_the_vehicle = Random.Range(1, 4);
                    Debug.Log("pętla " + i + "\n Wylosowało wartosc:" + colour_of_the_vehicle);
                    if (colour_of_the_vehicle == 1)
                    {
                        GameObject.Instantiate(vehicle_car_orange, wheretoplace_left, transform.rotation * Quaternion.Euler(0f, 0f, 270f));
                    }
                    if (colour_of_the_vehicle == 2)
                    {
                        GameObject.Instantiate(vehicle_car_red, wheretoplace_left, transform.rotation * Quaternion.Euler(0f, 0f, 270f));
                    }
                    if (colour_of_the_vehicle == 3)
                    {
                        GameObject.Instantiate(vehicle_car_blue, wheretoplace_left, transform.rotation * Quaternion.Euler(0f, 0f, 270f));
                    }
                    //break;
                }



                if (i % 4 == 2)
                {
                    wheretoplace_top = new Vector2(XshiftTop, YshiftTop);
                    int colour_of_the_vehicle = Random.Range(1, 4);
                    Debug.Log("pętla " + i + "\n Wylosowało wartosc:" + colour_of_the_vehicle);
                    if (colour_of_the_vehicle == 1)
                    {
                        GameObject.Instantiate(vehicle_car_orange, wheretoplace_top, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    }
                    if (colour_of_the_vehicle == 2)
                    {
                        GameObject.Instantiate(vehicle_car_red, wheretoplace_top, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    }
                    if (colour_of_the_vehicle == 3)
                    {
                        GameObject.Instantiate(vehicle_car_blue, wheretoplace_top, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    }
                    //break;
                }



                if (i % 4 == 3)
                {
                    wheretoplace_right = new Vector2(XshiftRight, YshiftRight);
                    int colour_of_the_vehicle = Random.Range(1, 4);
                    Debug.Log("pętla " + i + "\n Wylosowało wartosc:" + colour_of_the_vehicle);
                    if (colour_of_the_vehicle == 1)
                    {
                        GameObject.Instantiate(vehicle_car_orange, wheretoplace_right, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
                    }
                    if (colour_of_the_vehicle == 2)
                    {
                        GameObject.Instantiate(vehicle_car_red, wheretoplace_right, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
                    }
                    if (colour_of_the_vehicle == 3)
                    {
                        GameObject.Instantiate(vehicle_car_blue, wheretoplace_right, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
                    }
                    //break;
                }



                if (i % 4 == 0)
                {
                    wheretoplace_down = new Vector2(XshiftDown, YshiftDown);
                    int colour_of_the_vehicle = Random.Range(1, 4);
                    Debug.Log("pętla " + i + "\n Wylosowało wartosc:" + colour_of_the_vehicle);
                    if (colour_of_the_vehicle == 1)
                    {
                        GameObject.Instantiate(vehicle_car_orange, wheretoplace_down, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                    }
                    if (colour_of_the_vehicle == 2)
                    {
                        GameObject.Instantiate(vehicle_car_red, wheretoplace_down, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                    }
                    if (colour_of_the_vehicle == 3)
                    {
                        GameObject.Instantiate(vehicle_car_blue, wheretoplace_down, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                    }
                    //break;
                }
                nextSpawn = Time.time + spawnRate;
                i++;
            }
        
        
    }
}
