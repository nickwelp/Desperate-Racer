//SeekSteer.js
 
var waypoints : Transform[];
var waypointRadius : float = 1.5;
var damping : float = 0.1;
var loop : boolean = false;
var speed : float = 2.0;
var faceHeading : boolean = true;
var canDrive : boolean = false;
var canSteer : boolean = true;
var DrivePower : int = 5000000;

var MapMakerJS : createRaceWay;
 
private var targetHeading : Vector3;
private var currentHeading : Vector3;
private var targetwaypoint : int = 0;
private var xform : Transform;
private var useRigidbody : boolean;
private var rigidmember : Rigidbody;
 
var localWayPoints : Vector3[];
private var localGameObjects : GameObject[];
// Use this for initialization
function Start() {

var Tester : boolean =  (MapMakerJS.WayPoints.Length>0);
while(!Tester){	
	while(!Tester) yield;
	Tester = MapMakerJS.WayPoints.Length>0;
	yield WaitForSeconds(1.0);
}//won't stop tell MapMAker is done
localWayPoints = new Vector3[MapMakerJS.WayPoints.Length];
waypoints = new Transform[MapMakerJS.WayPoints.Length];
localGameObjects = new GameObject[MapMakerJS.WayPoints.Length];

xform = transform;
currentHeading = xform.forward;
var name1 : String;
for( var o :int = 0;o<localWayPoints.Length;o++){
	name1="WayPointSphere"+o.ToString();
	localGameObjects[o] = GameObject.Find(name1);
	waypoints[o] = localGameObjects[o].transform;
}
if(waypoints.Length<=0)
{
Debug.Log("No waypoints on "+name);
enabled = false;
}
targetwaypoint = 0;
if(rigidbody!=null)
{
useRigidbody = true;
rigidmember = rigidbody;
}
else
{
useRigidbody = false;
}


}
 
 
// calculates a new heading
function FixedUpdate() {
targetHeading = waypoints[targetwaypoint].position - xform.position;
//Debug.Log("targetwaypint: "+targetwaypoint.ToString()); 
currentHeading = Vector3.Lerp(currentHeading,targetHeading,damping*Time.deltaTime);
Check_If_Car_Is_Stuck();


 
if(useRigidbody){
//rigidmember.velocity = currentHeading * speed;

		//rigidmember.AddForce(transform.forward * Time.deltaTime * (throttle*5000000+ brakeForce));
	var throttle : int = 1;
	var brakeForce : int = 1;
	var mar : float = 1;
		if(!canDrive){
			mar = 0.1;
		}
	rigidmember.AddForce((transform.forward * Time.deltaTime * (throttle*DrivePower+ brakeForce))*mar);
}
if(faceHeading)
	//xform.LookAt(targetHeading);
  //rigidmember.AddTorque(targetHeading);
  xform.LookAt(xform.position+currentHeading);
 
if(Vector3.Distance(xform.position,waypoints[targetwaypoint].position)<=waypointRadius)
{
targetwaypoint++;
if(targetwaypoint>=waypoints.Length)
{
targetwaypoint = 0;
if(!loop)
enabled = false;
}
}
}
var resetTime : float = 5.0;
private var resetTimer : float = 0.0;

function Check_If_Car_Is_Stuck(){
	if(transform.forward.magnitude<2)
		resetTimer += Time.deltaTime;
	else
		resetTimer = 0;
	
	if(resetTimer > resetTime)
		MoveCar();
		//KillCar();
}

function OnCollisionEnter(collisionInfo : Collision){
 	if(collisionInfo.transform.name=="Road"){
 		canDrive = true;}
 
}

function OnCollisionExit(collisionInfo : Collision){
 	if(collisionInfo.transform.name=="Road"){
 		canDrive = false;}
 
}

 
  
    
function MoveCar(){
	var y : int = targetwaypoint-1;
	if(y<0){y=waypoints.Length-1;}
//	transform.position = Vector3(waypoints[y].position.x,waypoints[y].position.y+4,waypoints[y].position.z);
	
}
// draws red line from waypoint to waypoint
function OnDrawGizmos(){
 
Gizmos.color = Color.red;
for(var i : int = 0; i< waypoints.Length;i++)
{
var pos : Vector3 = waypoints[i].position;
if(i>0)
{
var prev : Vector3 = waypoints[i-1].position;
Gizmos.DrawLine(prev,pos);
}
}
}