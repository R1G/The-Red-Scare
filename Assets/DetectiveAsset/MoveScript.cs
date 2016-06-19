using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	Animator anim;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void Update() {
		float forward = Input.GetAxis ("Vertical");
		float turning = Input.GetAxis ("Horizontal");
		if (forward < -0.5f) {
			forward = -0.5f;
		}
		anim.SetFloat ("Speed", forward);
		anim.SetFloat ("rotation", turning);

		if (Input.GetButton ("Jump")) {
			transform.Translate (0, 0, forward * .2f);
			transform.Rotate (0, turning/2, 0);
			anim.SetBool ("isRunning", true);
			gameObject.GetComponentInChildren<Camera> ().fieldOfView = 50;
		} else {
			transform.Translate (0, 0, forward*.05f);
			transform.Rotate (0, turning, 0);
			anim.SetBool ("isRunning", false);
			gameObject.GetComponentInChildren<Camera> ().fieldOfView = 65;
		}

	} 
}
