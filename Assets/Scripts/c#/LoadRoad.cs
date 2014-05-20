using UnityEngine;
using System.Collections;

public class LoadRoad : MonoBehaviour {
    public GameObject roadLoader;
    public GameObject meshManager;
    public GameObject InsideWall;
    public GameObject OutsideWall;
    public GameObject Road;
    
    
    
    // Use this for initialization
	IEnumerator Start (){
        simpleDropDown setList = roadLoader.GetComponent<simpleDropDown>();
        MeshManager getList = meshManager.GetComponent<MeshManager>();     
        while(getList.fileCount==0){
            yield return new WaitForSeconds(1); 
        }
        setList.SetList(getList.ReturnFileList());
        LoadMeshFromWeb loadRoad = Road.GetComponent<LoadMeshFromWeb>();
        LoadMeshFromWeb loadInsideWall = InsideWall.GetComponent<LoadMeshFromWeb>();
        LoadMeshFromWeb loadOutsideWall = OutsideWall.GetComponent<LoadMeshFromWeb>();
        while (setList.indexNumber == 0) {
            yield return new WaitForSeconds(1); 
        }
        loadRoad.LoadMesh("Road",setList.list[setList.indexNumber]);
        loadInsideWall.LoadMesh("InsideWall", setList.list[setList.indexNumber]);
        loadOutsideWall.LoadMesh("OutsideWall", setList.list[setList.indexNumber]);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
