#pragma strict
var TurningRadius : float = 1.0;
var centerOfMass : Transform;
var Player : Transform;
var MoveSpeed : int = 99999;
var MaxDist = 0;
var MinDist = 100000;	
var multiplier : float = 10.0;
var GunCooler : int = 0.0;
var GunCoolerNorm : int = 20;

function Start () {

}

function Update () {
var relativeVelocity : Vector3 = transform.InverseTransformDirection(rigidbody.velocity);


transform.LookAt(Player);
 
if(Vector3.Distance(transform.position,Player.position) <= MinDist){
 
transform.position += transform.forward*MoveSpeed*Time.deltaTime * multiplier;
 
this.transform.position.y=1; 
 
if(Vector3.Distance(transform.position,Player.position) <= MaxDist)
{
if(GunCooler==0){
FireCube(relativeVelocity);
}else{GunCooler--;}


}
 
}
}



function FireCube(relativeVelocity){ 
    var go : GameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    go.transform.position = centerOfMass.transform.position + 4.5f * centerOfMass.transform.forward;
    go.transform.position.y = centerOfMass.transform.position.y;
    go.transform.rotation = centerOfMass.transform.rotation;
    var rb: Rigidbody = go.AddComponent(Rigidbody);
    go.renderer.material.color = Color.yellow;
   	rb.useGravity = false;
   	go.transform.localScale.x *= 0.25;
    go.transform.localScale.z *= 1.25;
    go.transform.localScale.y *= 0.25;
   	rb.mass = 50;
   	Destroy(go, 1);
    rb.AddRelativeForce(Vector3.forward*450000);	
    GunCooler = GunCoolerNorm;
   }
