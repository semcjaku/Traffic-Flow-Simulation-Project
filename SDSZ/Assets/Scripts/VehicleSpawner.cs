using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject vehicle;
    Vector2 wheretoplace;
    public float spawnRate = 4.0f;
    float nextSpawn = 4.0f;
    float XshiftLeft = -6.35f;
    float YshiftLeft = -0.39f;
    int NumberOfVehicles = 4;
    int VehicleCounter = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(VehicleCounter < NumberOfVehicles)
        {
            if (Time.time > nextSpawn)
            {

                nextSpawn = Time.time + spawnRate;
                wheretoplace = new Vector2(XshiftLeft, YshiftLeft);
                //XshiftLeft += 1.0f;
                GameObject.Instantiate(vehicle, wheretoplace, transform.rotation * Quaternion.Euler(0f, 0f, 270f));
                VehicleCounter += 1;
            }
        }
        
    }
}
