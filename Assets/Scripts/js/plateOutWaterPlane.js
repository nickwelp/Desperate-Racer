#pragma strict
var WaterPlate : Transform;
var yplane : float = 0.4;

function Start () {
for(var h = -16;h<16;h++){
	for(var t = -16;t<-16;t++){
			Instantiate(WaterPlate,  Vector3((50.0*h),yplane,(50.0*t)), Quaternion.identity);
		}
	}
}