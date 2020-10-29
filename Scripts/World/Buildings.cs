using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour {

    public GameObject wholeBuilding;
    //public GameObject splitBuilding;

    private GameObject player;

    private Rigidbody rgbdy;

	// Use this for initialization
	void Start () {
        rgbdy = gameObject.GetComponent<Rigidbody>();

        //splitBuilding.SetActive(false);

        player = GameObject.Find("PlayerHuman");
	}
	
	// Update is called once per frame
	void Update () {
            
	}

    public void pulledTowardsPlayer()
    {
        rgbdy.isKinematic = false;
        GameObject.Find("StretchBone").GetComponent<StretchArmScript>().StretchToPoint(gameObject.transform.position);
        rgbdy.AddForce((player.transform.position - gameObject.transform.position) * player.GetComponent<playermovement>().pullStrength);
    }

    public void Yeet(Vector3 yeetPoint)
    {
        rgbdy.AddForce((yeetPoint - gameObject.transform.position) * player.GetComponent<playermovement>().pullStrength * 10);
    }

}
