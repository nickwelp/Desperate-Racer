// Add this to any object. This will load it's
// mesh from the given URL.
 
//var url = "http://files.unity3d.com/aras/SerializedMesh.data";
var Name : String;



function Start(){
	var url = "file://" + Application.dataPath + "/Streaming Assets/meshes/"+ Name +".data"; 
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
        renderer.material.color = Color.white;
    }
    meshFilter.mesh = mesh;
}