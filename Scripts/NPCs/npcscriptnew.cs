using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcscriptnew : MonoBehaviour {

    private int moveForwardTime;
    public int speed;

    public int fearLevel;

    public GameObject showDistance;

    Rigidbody rgbdy;

    public int distanceForInbetweenCheck;

    Vector3 direction;

    // Use this for initialization
    void Start () {
        moveForwardTime = Random.Range(0, 100);
        rgbdy = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update() {
        rgbdy.velocity = new Vector3(0,rgbdy.velocity.y,0);
        float fallVelocity = rgbdy.velocity.y;
        if (moveForwardTime > 0 & fallVelocity == 0)
        {
            rgbdy.velocity += gameObject.transform.forward * speed;
            //rgbdy.velocity += Vector3.down * fallVelocity;
            moveForwardTime -= 1;
        }
        //showDistance.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}", rgbdy.velocity);

        if (moveForwardTime == 0)
        {
            checkDirection();
            moveForwardTime = Random.Range(0, 100);
        }
        checkForPlayerInView();
    }

    void checkDirection()
    {
        int directionNumber = Random.Range(0, 4);
        if (directionNumber == 1)
        {
            direction = -transform.right;
        }
        else if (directionNumber == 2)
        {
            direction = -transform.forward;
        }
        else if (directionNumber == 3)
        {
            direction = transform.right;
        }
        else if (directionNumber == 4)
        {
            direction = transform.forward;
        }
        float distance = 100;
        bool objectInBetween = (Physics.Raycast(transform.position, direction, (distance)));
        Debug.DrawRay(transform.position, direction, Color.red, 5);
        //Debug.Log(objectInBetween);

        if (objectInBetween)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.gameObject.tag == "Wall")
                {
                    //Debug.Log("Wall");
                }
                else
                {
                    gameObject.transform.Rotate(new Vector3(0, directionNumber * 90,0));
                }
            }
            else
            {
                gameObject.transform.Rotate(new Vector3(0, directionNumber * 90, 0));
            }
        }
        else
        {
            gameObject.transform.Rotate(new Vector3(0, directionNumber * 90, 0));
        }
    }
    public void checkForPlayerInView()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
}
