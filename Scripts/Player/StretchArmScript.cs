using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchArmScript : MonoBehaviour
{
    Vector3 startposition;

    // Use this for initialization
    void Start()
    {
        startposition = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StretchToPoint(Vector3 pullPoint)
    {
        gameObject.transform.position = pullPoint;
    }

    public void ReturnToDefult()
    {
        gameObject.transform.localPosition = startposition;
    }
}
