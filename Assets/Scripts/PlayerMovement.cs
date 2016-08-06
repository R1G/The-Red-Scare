using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	//This is in units
	public float forwardSpeed;
	//This is in degrees
	public float rotationalSpeed;
	Animator anim;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void Update() {
		float horizontal;
		float vertical;
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		//Added time.deltatime. calculation below should work out to meters per frame
		transform.Translate (vertical*forwardSpeed*Time.deltaTime, 0, 0 );
		transform.Rotate (0, horizontal*rotationalSpeed*Time.deltaTime, 0);

		anim.SetFloat ("speed", vertical);
	}
}
