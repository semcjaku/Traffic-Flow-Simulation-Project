using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text numberOfCars,averageWaitingTime;
    private VehicleSpawner cs;
    void Start()
    {
        cs = GameObject.Find("CarSpawner").GetComponent<VehicleSpawner>();
        numberOfCars.text = "dupa";// cs.NumberOfVehicles.ToString();
        averageWaitingTime.text = (Crossroads.totalWaitingTime / cs.NumberOfVehicles).ToString();
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}