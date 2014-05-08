
var dragMultiplier : Vector3 = new Vector3(2, 5, 1);

var throttle : float = 0; 
private var steer : float = 0;
private var handbrake : boolean = false;

var _GameManager : GameManager;

var centerOfMass : Transform;
public var CentMass : Transform;
var theBullet : Transform;
var topSpeed : float = 160;
var numberOfGears : int = 5;
var DrivePower : int = 5000000;
var Govenor : float = 100.0;
var maximumTurn : int = 15;
var minimumTurn : int = 10;
var GunCooler : int = 0;
var GunCoolerNorm: int = 20;
var resetTime : float = 5.0;
private var resetTimer : float = 0.0;

private var engineForceValues : float[];
private var gearSpeeds : float[];

private var currentGear : int;
private var currentEnginePower : float = 0.0;

private var handbrakeXDragFactor : float = 0.5;
private var initialDragMultiplierX : float = 10.0;
private var handbrakeTime : float = 0.0;
private var handbrakeTimer : float = 1.0;

var mphNeedle : RotatableGuiItem;
		
/*
private var skidmarks : Skidmarks = null;
private var skidSmoke : ParticleEmitter = null;
var skidmarkTime : float[];
*/
private var sound : SoundController = null;
sound = transform.GetComponent(SoundController);

private var canSteer : boolean;
private var canDrive : boolean;


function Start()
{	
	// Measuring 1 - 60
	var accelerationTimer = Time.time;
	InitializeState();
	
	SetupCenterOfMass();
	
	topSpeed = Convert_Miles_Per_Hour_To_Meters_Per_Second(topSpeed);
	
	SetupGears();
	
	
	initialDragMultiplierX = dragMultiplier.x;
}

function Update()
{		
	var relativeVelocity : Vector3 = transform.InverseTransformDirection(rigidbody.velocity);
	
	GetInput();
	 rigidbody.constraints=RigidbodyConstraints.FreezeRotationY;
	
	Check_If_Car_Is_Flipped();
	PauseControls();
	UpdateGear(relativeVelocity);
	
	if(GunCooler==0){
		FireCube(relativeVelocity);
	}else{GunCooler--;}
	
 }

function FixedUpdate()
{	
	// The rigidbody velocity is always given in world space, but in order to work in local space of the car model we need to transform it first.
	var relativeVelocity : Vector3 = transform.InverseTransformDirection(rigidbody.velocity);
	var mph = rigidbody.velocity.magnitude * 2.237;
	//mphNeedle.angle = mph;
	mphNeedle.angle = 20 + mph * 1.4f;
	
	
		
	UpdateDrag(relativeVelocity);
	
	CalculateEnginePower(relativeVelocity);
	
	ApplyThrottle(canDrive, relativeVelocity);
    //this.transform.position.y = 1;
	
	ApplySteering(canSteer, relativeVelocity);
}

/**************************************************/
/* Functions called from Start()                  */
/**************************************************/

function PauseControls(){
	if (Input.GetKey("p")){
		_GameManager.Instance.ChangeState("PAUSED");
		_GameManager.Instance.Pause(true);
		}
	if(Input.GetKey("i")){
		//_GameManager.Instance.ChangeState("RACING");
		_GameManager.Instance.Pause(false);
	
	}

}
function SetupCenterOfMass()
{
	if(centerOfMass != null)
		rigidbody.centerOfMass = centerOfMass.localPosition;
		CentMass = centerOfMass;
}

function SetupGears()
{
	engineForceValues = new float[numberOfGears];
	gearSpeeds = new float[numberOfGears];
	
	var tempTopSpeed : float = topSpeed;
		
	for(var i = 0; i < numberOfGears; i++)
	{
		if(i > 0)
			gearSpeeds[i] = tempTopSpeed / 4 + gearSpeeds[i-1];
		else
			gearSpeeds[i] = tempTopSpeed / 4;
		
		tempTopSpeed -= tempTopSpeed / 4;
	}
	
	var engineFactor : float = topSpeed / gearSpeeds[gearSpeeds.Length - 1];
	
	for(i = 0; i < numberOfGears; i++)
	{
		var maxLinearDrag : float = gearSpeeds[i] * gearSpeeds[i];// * dragMultiplier.z;
		engineForceValues[i] = maxLinearDrag * engineFactor;
	}
}

/**************************************************/
/* Functions called from Update()                 */
/**************************************************/

function FireCube(relativeVelocity){ 
   if (Input.GetKey("z"))
    {
    rigidbody.constraints=RigidbodyConstraints.FreezeRotationY;
	
    Instantiate(theBullet,  centerOfMass.transform.position + 4.5f * centerOfMass.transform.forward, transform.rotation);
    
    /*
    //go.tag = "Bullet";
    //var rb: Rigidbody = go.AddComponent(Rigidbody);
    //go.renderer.material.color = Color.yellow;
   	rb.useGravity = false;
   	//rb.isTrigger = true;
   	
   	go.transform.localScale.x *= 1;
    go.transform.localScale.z *= 1.25;
    go.transform.localScale.y *= 1;
   	rb.mass = 1000;
   	Destroy(go, 100);
    rb.AddRelativeForce(Vector3.forward*4500000);	
    */
    GunCooler = GunCoolerNorm;
    }
   }

function GetInput()
{
	throttle = Input.GetAxis("Vertical");
	steer = Input.GetAxis("Horizontal");
	if(steer<0.0){
	
	}else{
	
	}
	
	
}

function Check_If_Car_Is_Flipped()
{
	if(transform.localEulerAngles.z > 80 && transform.localEulerAngles.z < 280)
		resetTimer += Time.deltaTime;
	else
		resetTimer = 0;
	
	if(resetTimer > resetTime)
		FlipCar();
}

function FlipCar()
{
	transform.rotation = Quaternion.LookRotation(transform.forward);
	transform.position += Vector3.up * 0.5;
	rigidbody.velocity = Vector3.zero;
	rigidbody.angularVelocity = Vector3.zero;
	resetTimer = 0;
	currentEnginePower = 0;
}


function UpdateGear(relativeVelocity : Vector3)
{
	currentGear = 0;
	for(var i = 0; i < numberOfGears - 1; i++)
	{
		if(relativeVelocity.z > gearSpeeds[i])
			currentGear = i + 1;
	}
}

 



/**************************************************/
/* Functions called from FixedUpdate()            */
/**************************************************/

function UpdateDrag(relativeVelocity : Vector3)
{
	var relativeDrag : Vector3 = new Vector3(	-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), 
												-relativeVelocity.y * Mathf.Abs(relativeVelocity.y), 
												-relativeVelocity.z * Mathf.Abs(relativeVelocity.z) );
	
	var drag = Vector3.Scale(dragMultiplier, relativeDrag);
		
	if(initialDragMultiplierX > dragMultiplier.x) // Handbrake code
	{			
		drag.x /= (relativeVelocity.magnitude / (topSpeed / ( 1 + 2 * handbrakeXDragFactor ) ) );
		drag.z *= (1 + Mathf.Abs(Vector3.Dot(rigidbody.velocity.normalized, transform.forward)));
		drag += rigidbody.velocity * Mathf.Clamp01(rigidbody.velocity.magnitude / topSpeed);
	}
	else // No handbrake
	{
		drag.x *= topSpeed / relativeVelocity.magnitude;
	}
	
	if(Mathf.Abs(relativeVelocity.x) < 5 && !handbrake)
		drag.x = -relativeVelocity.x * dragMultiplier.x;
		

	rigidbody.AddForce(transform.TransformDirection(drag) * rigidbody.mass * Time.deltaTime);
}


function CalculateEnginePower(relativeVelocity : Vector3)
{
	if(throttle == 0)
	{
		currentEnginePower -= Time.deltaTime * 200;
	}
	else if( HaveTheSameSign(relativeVelocity.z, throttle) )
	{
		var normPower = (currentEnginePower / engineForceValues[engineForceValues.Length - 1]) * 2;
		currentEnginePower += Time.deltaTime * 200 * EvaluateNormPower(normPower);
	}
	else
	{
		currentEnginePower -= Time.deltaTime * 300;
	}
	
	if(currentGear == 0)
		currentEnginePower = Mathf.Clamp(currentEnginePower, 0, engineForceValues[0]);
	else
		currentEnginePower = Mathf.Clamp(currentEnginePower, engineForceValues[currentGear - 1], engineForceValues[currentGear]);
}

function InitializeState()
{
	canDrive = false;
	canSteer = true;
	
}
function OnCollisionEnter(collisionInfo : Collision){
 	if(collisionInfo.transform.name=="Road"){
 		canDrive = true;}
 
}

function OnCollisionExit(collisionInfo : Collision){
 	if(collisionInfo.transform.name=="Road"){
 		canDrive = false;}
 
}


function ApplyThrottle(canDrive : boolean, relativeVelocity : Vector3)
{	
		var mar : float = 1;
		if(!canDrive){
			mar = 0.1;
		}
		var throttleForce : float = 0;
		var brakeForce : float = 0;
		rigidbody.AddForce((transform.forward * Time.deltaTime * (throttle*DrivePower+ brakeForce))*mar);
}

function ApplySteering(canSteer : boolean, relativeVelocity : Vector3)
{
		var turnRadius : float = 3.0 / Mathf.Sin((90 - (steer * 30)) * Mathf.Deg2Rad);
		var minMaxTurn : float = EvaluateSpeedToTurn(rigidbody.velocity.magnitude);
		var turnSpeed : float = Mathf.Clamp(relativeVelocity.z / turnRadius, -minMaxTurn / 10, minMaxTurn / 10);
		
		transform.RotateAround(	transform.position + transform.right * turnRadius * steer, 
								transform.up, 
								turnSpeed * Mathf.Rad2Deg * Time.deltaTime * steer);
		
		var debugStartPoint = transform.position + transform.right * turnRadius * steer;
		var debugEndPoint = debugStartPoint + Vector3.up * 5;
		
		Debug.DrawLine(debugStartPoint, debugEndPoint, Color.red);
		
		if(initialDragMultiplierX > dragMultiplier.x) // Handbrake
		{
			var rotationDirection : float = Mathf.Sign(steer); // rotationDirection is -1 or 1 by default, depending on steering
			if(steer == 0)
			{
				if(rigidbody.angularVelocity.y < 1) // If we are not steering and we are handbraking and not rotating fast, we apply a random rotationDirection
					rotationDirection = Random.Range(-1.0, 1.0);
				else
					rotationDirection = rigidbody.angularVelocity.y; // If we are rotating fast we are applying that rotation to the car
			}
			// -- Finally we apply this rotation around a point between the cars front wheels.
		
		}
	
}

/**************************************************/
/*               Utility Functions                */
/**************************************************/

function Convert_Miles_Per_Hour_To_Meters_Per_Second(value : float) : float
{
	return value * 0.44704;
}

function Convert_Meters_Per_Second_To_Miles_Per_Hour(value : float) : float
{
	return value * 2.23693629;	
}

function HaveTheSameSign(first : float, second : float) : boolean
{
	if (Mathf.Sign(first) == Mathf.Sign(second))
		return true;
	else
		return false;
}

function EvaluateSpeedToTurn(speed : float)
{
	if(speed > topSpeed / 2)
		return minimumTurn;
	
	var speedIndex : float = 1 - (speed / (topSpeed / 2));
	return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
}

function EvaluateNormPower(normPower : float)
{
	if(normPower < 1)
		return 10 - normPower * 9;
	else
		return 1.9 - normPower * 0.9;
}

function GetGearState()
{
	var relativeVelocity : Vector3 = transform.InverseTransformDirection(rigidbody.velocity);
	var lowLimit : float = (currentGear == 0 ? 0 : gearSpeeds[currentGear-1]);
	return (relativeVelocity.z - lowLimit) / (gearSpeeds[currentGear - lowLimit]) * (1 - currentGear * 0.1) + currentGear * 0.1;
}