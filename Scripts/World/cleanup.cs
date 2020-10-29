using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleanup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<BoxCollider>().size = new Vector3(gameObject.GetComponent<BoxCollider>().size.x * 0.2f, gameObject.GetComponent<BoxCollider>().size.y * 0.2f, gameObject.GetComponent<BoxCollider>().size.z * 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
        if (distanceBetweenPlayerAndObject() > 500)
        {
            Destroy(gameObject);
        }
	}

    public float distanceBetweenPlayerAndObject()
    {
        return Vector3.Distance(gameObject.transform.position, GameObject.Find("PlayerHuman").transform.position);
    }
}
