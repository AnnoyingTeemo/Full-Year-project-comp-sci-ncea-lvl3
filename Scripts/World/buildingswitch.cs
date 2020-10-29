using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingswitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void triggerGrapple()
    {
        gameObject.GetComponentInParent<Buildings>().pulledTowardsPlayer();
    }

    public void TriggerYeet(Vector3 YeetPoint)
    {
        gameObject.GetComponentInParent<Buildings>().Yeet(YeetPoint);
    }
}
