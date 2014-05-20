// Add this to any object. This will load it's
// mesh from the given URL.
 
//var url = "http://files.unity3d.com/aras/SerializedMesh.data";
var substance: Material;

 function LoadMesh(Name: String, dirName:String){
	var url =  "file://" + Application.dataPath + "/Streaming Assets/Meshes/"+ dirName + "/"+ Name +".data"; 
	Debug.Log(url + " XXXHere");
	DoTheLoading(url);
}
function DoTheLoading(url){
	var download = WWW(url);
    yield download;
    var mesh = MeshSerializer.ReadMeshFromWWW( download );
    if (!mesh)
    {
        print ("Failed to load mesh");
        return;
    }
    print ("Mesh loaded");
 
    var meshFilter : MeshFilter = GetComponent(MeshFilter);
    if( !meshFilter ) {
        meshFilter = gameObject.AddComponent(MeshFilter);
        gameObject.AddComponent("MeshRenderer");
        renderer.material = substance;
    }
    meshFilter.mesh = mesh;
}