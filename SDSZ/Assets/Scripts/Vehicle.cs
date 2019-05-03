using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Vehicles", menuName = "Vehicle/Car")]
public class Vehicle : ScriptableObject
{
    public string objectName = "New Vehicle";    

    public float currentSpeed;
    public float xSpeedCompound;
    public float ySpeedCompound;
    public float currentAcceleration;

    public bool stayBlocade_fl = false;
    public bool canAccelerate_fl = true;

    Image image;


}
