using UnityEngine;
using System.Collections;
using System.IO;

public class MeshManager : MonoBehaviour {
    private string LargestName;
    private DirectoryInfo[] info;
    private DirectoryInfo dir;
    public int fileCount = 0;
    public string[] listOfFiles;
    
	// Use this for initialization
    void Start()
    {
        string MeshDirectory = Application.dataPath + "/Streaming Assets/Meshes/";
        dir = new DirectoryInfo(MeshDirectory);
        info = dir.GetDirectories();
        fileCount = info.Length+1;
        int y = 1;
        listOfFiles = new string[info.Length+1];
        listOfFiles[0] = "Select Map";
        foreach (DirectoryInfo f in info)
        {
            listOfFiles[y] = f.Name;
            y++;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //public static void Save
    public string[] ReturnFileList() {
        return listOfFiles;
    }
    public static void DeleteSubDirectoryByName(string Name) {
        string myPath = Application.dataPath + "/" + Name;
        Directory.Delete(myPath, true);
    }

}
