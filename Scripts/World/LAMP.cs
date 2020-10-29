using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAMP : MonoBehaviour {

    public bool isGrappled;

    private Rigidbody rgbd;

	// Use this for initialization
	void Start () {
        rgbd = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrappled)
        {
            rgbd.isKinematic = false;
        }
	}
}
