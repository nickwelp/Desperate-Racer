using UnityEngine;
using System.Collections;

public class LoadRoad : MonoBehaviour {
    public GameObject roadLoader;
    public GameObject meshManager;
	// Use this for initialization
	IEnumerator Start (){
        simpleDropDown setList = roadLoader.GetComponent<simpleDropDown>();
        MeshManager getList = meshManager.GetComponent<MeshManager>();     
        while(getList.fileCount==0){
            Debug.Log("In the lo");
            yield return new WaitForSeconds(1); 
        }
        Debug.Log("Afterild");
        setList.SetList(getList.ReturnFileList());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
