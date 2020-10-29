using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour {
    Camera cam;
    public float Axis;
    public float maxRotation;
    public float minRotation;

    public float rotationY; public float sensitivityY = 1;
    // Use this for initialization
    void Start () {
        transform.rotation = Quaternion.identity;
        cam = gameObject.GetComponent<Camera>();

        
    }

    // Update is called once per frame
    void Update() {
        if (gameObject.GetComponent<PauseMenu>().paused == false)
        {
            GameObject player = GameObject.Find("PlayerHuman");
            LockedRotation();
        }

    }

    void LockedRotation()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * 10;
        rotationY = Mathf.Clamp(rotationY, -minRotation, maxRotation);

        transform.localEulerAngles = new Vector3(-rotationY, -90, transform.localEulerAngles.z);
    }
}
