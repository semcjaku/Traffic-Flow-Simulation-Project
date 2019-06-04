using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSpawner : MonoBehaviour
{
    public Slider slider;
    public GameObject vehicle_car_orange;
    public GameObject vehicle_car_red;
    public GameObject vehicle_car_blue;

    private GameObject temp_object;

    public float spawnRate = 4.0f;

    float nextSpawn = 4.0f;

    float XshiftLeft = -9f;
    float YshiftLeft = -0.39f;

    float XshiftRight = +9f;
    float YshiftRight = +0.39f;

    float XshiftTop = -0.39f;
    float YshiftTop = +6f;

    float XshiftDown = +0.39f;
    float YshiftDown = -6f;

    Vector2 wheretoplace_top;
    Vector2 wheretoplace_down;
    Vector2 wheretoplace_left;
    Vector2 wheretoplace_right;

    public int NumberOfVehicles;
    int randomPosition;
    int colour_of_the_vehicle;
    int i = 0;
    int k = 0;

    public List<GameObject> existing_cars;


    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("Car_Amount_slider").GetComponent<Slider>();
        NumberOfVehicles = (int)slider.value;
        List<GameObject> existing_cars = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {      
        if (Time.time > nextSpawn && i<NumberOfVehicles)
        {
            randomPosition = Random.Range(1, 4);
            switch (randomPosition)
            {
                case 1:
                    wheretoplace_left = new Vector2(XshiftLeft, YshiftLeft);
                    InstantiateVehicle(wheretoplace_left, 270f);
                    break;
                case 2:
                    wheretoplace_top = new Vector2(XshiftTop, YshiftTop);
                    InstantiateVehicle(wheretoplace_top, 180f);
                    break;
                case 3:
                    wheretoplace_right = new Vector2(XshiftRight, YshiftRight);
                    InstantiateVehicle(wheretoplace_right, 90f);
                    break;
                case 4:
                    wheretoplace_down = new Vector2(XshiftDown, YshiftDown);
                    InstantiateVehicle(wheretoplace_down, 0f);
                    break;
            }
            nextSpawn = Time.time + spawnRate;
            i++;
            slider.value--;
        }
    }

    void InstantiateVehicle(Vector2 wheretoplace, float angle)
    {
        colour_of_the_vehicle = Random.Range(1, 4);
        switch(colour_of_the_vehicle)
        {
            case 1:
                temp_object = GameObject.Instantiate(vehicle_car_orange, wheretoplace, transform.rotation * Quaternion.Euler(0f, 0f, angle));
                break;
            case 2:
                temp_object = GameObject.Instantiate(vehicle_car_red, wheretoplace, transform.rotation * Quaternion.Euler(0f, 0f, angle));
                break;
            case 3:
                temp_object = GameObject.Instantiate(vehicle_car_blue, wheretoplace, transform.rotation * Quaternion.Euler(0f, 0f, angle));
                break;
        }
        existing_cars.Add(temp_object);
    }
}
