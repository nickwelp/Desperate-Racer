using UnityEngine;
using System.Collections;

public class TestRaceTrack : MonoBehaviour {
    //public InsideWall as GameObject;
   // public OutsideWall as GameObject;
    public GameObject[] RoadWay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 80, 20), "Save Map")){
        //   InsideWall = GameObject.Find('InsideWall');
        //   OutsideWall = GameObject.Find('OutsideWall');

            SaveMap();
        }
        if (GUI.Button(new Rect(20, 70, 80, 20), "Reload Map"))
        {
            Application.LoadLevel("_Map Maker");
        }

    }
    void SaveMap()
    {
              RoadWay = GameObject.FindGameObjectsWithTag("Road");
	        //add SaveMeshForWeb.js to Roadway, InsideWall and Outsidewall
       //     InsideWall.AddComponent("SaveMeshForWeb");
        //    OutsideWall.AddComponent("SaveMeshForWeb");
            foreach(GameObject Road in RoadWay){
                Road.AddComponent("SaveMeshForWeb");
                }
    }
}
