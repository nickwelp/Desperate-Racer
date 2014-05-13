// Add this to any object. This will save it's mesh
// to a local file for the Web.
 
var fileName;
var saveTangents = false;
 
function SaveThis(Name : String)
{
	fileName = Name + ".data";
    var inputMesh = GetComponent(MeshFilter).mesh;
    var fullFileName = Application.dataPath + "/Streaming Assets/Meshes/" + fileName;
    MeshSerializer.WriteMeshToFileForWeb (inputMesh, fullFileName, saveTangents);
    print ("Saved " + name + " mesh to " + fullFileName );
}