using UnityEngine;
using System.Collections;
using System.IO;

public class MeshManager : MonoBehaviour {
    private string LargestName;
    private FileInfo[] info;
    private DirectoryInfo dir;
    public int fileCount;
    public  string[] listOfFiles;
    private int y = 0;
	// Use this for initialization
    void Start()
    {
        string MeshDirectory = Application.dataPath + "/Streaming Assets/Meshes/";
        dir = new DirectoryInfo(MeshDirectory);
        info = dir.GetFiles("*.");
        fileCount = info.Length;
        listOfFiles = new string[info.Length];
        foreach (FileInfo f in info)
        {
            listOfFiles[y] = f.Name;
            y++;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //public static void Save

    public static void DeleteSubDirectoryByName(string Name) {
        string myPath = Application.dataPath + "/" + Name;
        Directory.Delete(myPath, true);
    }

}
