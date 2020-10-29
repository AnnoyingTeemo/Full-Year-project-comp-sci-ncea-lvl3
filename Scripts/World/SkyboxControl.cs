using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxControl : MonoBehaviour {

    public Material daySky;

    public Material nightsky;

    // Use this for initialization
    void Start () {
        if (System.DateTime.Now.Hour >= 18 || System.DateTime.Now.Hour <= 6)
        {
            RenderSettings.skybox = nightsky;
        }
        else
        {
            RenderSettings.skybox = daySky;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
