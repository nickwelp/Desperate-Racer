#pragma strict

var dragMultiplier : Vector3 = new Vector3(2, 5, 1);
private var initialDragMultiplierX : float = 10.0;
var topSpeed : float = 160;


function Start () {
		initialDragMultiplierX = dragMultiplier.x;

}

function Update () {
	
}


function FixedUpdate(){
		var relativeVelocity : Vector3 = transform.InverseTransformDirection(rigidbody.velocity);
		UpdateDrag(relativeVelocity);


}

function UpdateDrag(relativeVelocity : Vector3)
{
	var relativeDrag : Vector3 = new Vector3(	-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), 
												-relativeVelocity.y * Mathf.Abs(relativeVelocity.y), 
												-relativeVelocity.z * Mathf.Abs(relativeVelocity.z) );
	
	var drag = Vector3.Scale(dragMultiplier, relativeDrag);
		
	drag.x *= topSpeed / relativeVelocity.magnitude;
	
	if(Mathf.Abs(relativeVelocity.x) < 5 )
		drag.x = -relativeVelocity.x * dragMultiplier.x;
		

	rigidbody.AddForce(transform.TransformDirection(drag) * rigidbody.mass * Time.deltaTime);
}