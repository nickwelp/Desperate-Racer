using UnityEngine;
using System.Collections;

public class LoadRoad : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        var meshMang = new MeshManager();
        simpleDropDown.SetList(meshMang.listOfFiles);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
