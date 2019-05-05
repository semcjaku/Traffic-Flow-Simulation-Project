using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLightControlling : MonoBehaviour
{

    public GameObject basic_street_light1;
    public GameObject basic_street_light2;
    public GameObject basic_street_light3;
    public GameObject basic_street_light4;

    float XshiftLeft = -0.8f;
    float YshiftLeft = -0.8f;

    float XshiftRight = +0.8f;
    float YshiftRight = +0.8f;

    float XshiftTop = -0.8f;
    float YshiftTop = +0.8f;

    float XshiftDown = +0.8f;
    float YshiftDown = -0.8f;

    Vector2 wheretoplace_top;
    Vector2 wheretoplace_down;
    Vector2 wheretoplace_left;
    Vector2 wheretoplace_right;
    // Start is called before the first frame update
    void Start()
    {
        wheretoplace_left = new Vector2(XshiftLeft, YshiftLeft);
        wheretoplace_top = new Vector2(XshiftTop, YshiftTop);
        wheretoplace_right = new Vector2(XshiftRight, YshiftRight);
        wheretoplace_down = new Vector2(XshiftDown, YshiftDown);

        GameObject.Instantiate(basic_street_light1, wheretoplace_left, transform.rotation * Quaternion.Euler(0f, 0f, 270f));
        GameObject.Instantiate(basic_street_light2, wheretoplace_top, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
        GameObject.Instantiate(basic_street_light3, wheretoplace_right, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
        GameObject.Instantiate(basic_street_light4, wheretoplace_down, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
