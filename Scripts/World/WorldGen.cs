using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour {

    private GameObject building;

    private GameObject building2;

    private GameObject npc;

    private float normalHeight = 42;

    public float distanceBetweenBuildings;

    public int squareAmountOfBuildings;

    private GameObject lamp;
    public float lampHeight;

    public float NpcChance;

    // Use this for initialization
    void Start () {
        building = GameObject.Find("Building");

        building2 = GameObject.Find("BuildingsCombo");

        npc = GameObject.Find("NPC");
        lamp = GameObject.Find("lamp");
        for (var i = 0; i < squareAmountOfBuildings; i++)
        {
            for (var e = 0; e < squareAmountOfBuildings; e++)
            {
                float heightMultiplier = Random.Range(0.7f, 2.0f);
                GameObject newBuilding = Instantiate(building2, new Vector3((distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.x) * e, normalHeight, (distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.z) * i), Quaternion.identity);
                newBuilding.transform.localScale = new Vector3(newBuilding.transform.localScale.x, heightMultiplier * 5, newBuilding.transform.localScale.z);
                newBuilding.transform.position = new Vector3(newBuilding.transform.position.x, (normalHeight - 17) * heightMultiplier, newBuilding.transform.position.z);

                if (Random.Range(0, (int)(100 * 1/NpcChance)) == 1)
                {
                    GameObject newNpc = Instantiate(npc, new Vector3((distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.x) * e + distanceBetweenBuildings / 2 + building.GetComponent<BoxCollider>().size.x / 2, 5, (distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.z) * i), Quaternion.identity);
                }

                GameObject newLamp = Instantiate(lamp, new Vector3((distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.x) * e + distanceBetweenBuildings / 8 + building.GetComponent<BoxCollider>().size.x / 2, lampHeight, (distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.z) * i + (building.GetComponent<BoxCollider>().size.z/4)), Quaternion.Euler(new Vector3(0, 90, 0)));
                GameObject newLamp2 = Instantiate(lamp, new Vector3((distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.x) * e + distanceBetweenBuildings / 8 + building.GetComponent<BoxCollider>().size.x / 2, lampHeight, (distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.z) * i - (building.GetComponent<BoxCollider>().size.z / 4)), Quaternion.Euler(new Vector3(0, 90, 0)));

                GameObject newLamp3 = Instantiate(lamp, new Vector3((distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.x) * e + distanceBetweenBuildings / 8 + distanceBetweenBuildings / 2 + building.GetComponent<BoxCollider>().size.x / 16 + building.GetComponent<BoxCollider>().size.x / 2, lampHeight, (distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.z) * i + (building.GetComponent<BoxCollider>().size.z / 4)), Quaternion.Euler(new Vector3(0, -90, 0)));
                GameObject newLamp4 = Instantiate(lamp, new Vector3((distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.x) * e + distanceBetweenBuildings / 8 + distanceBetweenBuildings/2 + building.GetComponent<BoxCollider>().size.x / 16 + building.GetComponent<BoxCollider>().size.x / 2, lampHeight, (distanceBetweenBuildings + building.GetComponent<BoxCollider>().size.z) * i - (building.GetComponent<BoxCollider>().size.z / 4)), Quaternion.Euler(new Vector3(0, -90, 0)));
            }


        }

        npc.SetActive(false);
        building.SetActive(false);
        lamp.SetActive(false);
        building2.SetActive(false);

        GameObject floor = GameObject.Find("TempFloor");
        floor.transform.localScale = new Vector3(60 * squareAmountOfBuildings, 1, 60 * squareAmountOfBuildings);
        floor.transform.position = new Vector3((building.GetComponent<BoxCollider>().size.x + distanceBetweenBuildings) * (squareAmountOfBuildings/2), 0, (building.GetComponent<BoxCollider>().size.z + distanceBetweenBuildings) * (squareAmountOfBuildings / 2));
        
    }
	
	// Update is called once per frame
	void Update () {
	}
}
