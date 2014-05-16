using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class TestRaceTrack : MonoBehaviour {
    public GameObject[] InsideWall;
    public GameObject[] OutsideWall;
    public GameObject[] RoadWay;
    public SaveMeshForWeb other;
    private bool GetName = false;
	// Use this for initialization
    private string NameTheTrack = "NameThisTrack";
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (!GetName)
        {
            if (GUI.Button(new Rect(20, 40, 80, 20), "Save Map"))
            {
                //SaveMap();
                GetName = true;

            }
            if (GUI.Button(new Rect(20, 70, 80, 20), "Reload Map"))
            {
                Application.LoadLevel("_Map Maker");
            }
        }
        else if (GetName)
        {
            NameTheTrack = GUILayout.TextField(NameTheTrack, 20);
            if (GUI.Button(new Rect(20, 40, 80, 20), "Ok"))
            {
                NameTheTrack = Regex.Replace(NameTheTrack, "[^a-zA-Z0-9_.]+", "");
                SaveMap(NameTheTrack);
            }


        }

    }
    void SaveMap(string DirectoryName)
    {
              RoadWay = GameObject.FindGameObjectsWithTag("Road");
              InsideWall = GameObject.FindGameObjectsWithTag("Inside Wall");
              OutsideWall = GameObject.FindGameObjectsWithTag("Outside Wall");
              Directory.CreateDirectory(Application.dataPath + "/Streaming Assets/Meshes/"+DirectoryName);
	          foreach(GameObject Road in RoadWay){
                Road.AddComponent("SaveMeshForWeb");
                other = Road.GetComponent<SaveMeshForWeb>();
                other.SaveThis("Road", DirectoryName);
                }
              foreach (GameObject IWall in InsideWall)
              {
                  IWall.AddComponent("SaveMeshForWeb");
                  other = IWall.GetComponent<SaveMeshForWeb>();
                  other.SaveThis("InsideWall", DirectoryName);
              }
              foreach (GameObject OWall in OutsideWall)
              {
                  OWall.AddComponent("SaveMeshForWeb");
                  other = OWall.GetComponent<SaveMeshForWeb>();
                  other.SaveThis("OutsideWall", DirectoryName);
              }

    }
}
