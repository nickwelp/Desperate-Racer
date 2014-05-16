using UnityEngine;
using System.Collections;

public class BlowUpBullet : MonoBehaviour {
		
	public GameObject explosion;
	void Start(){
	//	this.transform.position.y+1;
	}
	// The target marker.

	public float speed = 2.0f;
	void Update() {
		//float step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position,(transform.position.x,transform.position.y,transform.position.z+2), step);
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision) {
		Instantiate(explosion, transform.position, transform.rotation);
	//	Destroy(other.gameObject);
	   Destroy(gameObject);
	}
}
