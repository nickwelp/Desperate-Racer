using UnityEngine;
using System.Collections;
using System.IO;

public class MeshManager : MonoBehaviour {
    private string LargestName;
    private FileInfo[] info;
    private DirectoryInfo dir;
    public int fileCount = 0;
    public string[] listOfFiles;
    
	// Use this for initialization
    void Start()
    {
        Debug.Log("State MeshManger");
        string MeshDirectory = Application.dataPath + "/Streaming Assets/Meshes/";
        Debug.Log(Application.dataPath);
        dir = new DirectoryInfo(MeshDirectory);
        info = dir.GetFiles("*.*");
        fileCount = info.Length;
        int y = 0;
        listOfFiles = new string[info.Length];
        foreach (FileInfo f in info)
        {
            Debug.Log(f.Name);
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
