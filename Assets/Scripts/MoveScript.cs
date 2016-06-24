using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	Animator anim;
	public float walkSpeed;
	public float runSpeed;

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
			transform.Translate (0, 0, forward * runSpeed);
			transform.Rotate (0, turning/2, 0);
			anim.SetFloat ("Speed", forward*2);
			gameObject.GetComponentInChildren<Camera> ().fieldOfView = Mathf.Lerp(gameObject.GetComponentInChildren<Camera> ().fieldOfView,80,0.1f);
		} else {
			transform.Translate (0, 0, forward*walkSpeed);
			transform.Rotate (0, turning, 0);
			if (gameObject.GetComponentInChildren<Camera> ().fieldOfView != 65) {
				gameObject.GetComponentInChildren<Camera> ().fieldOfView = Mathf.Lerp (gameObject.GetComponentInChildren<Camera> ().fieldOfView, 65, 0.1f);
			}
		}

		if (Input.GetMouseButton (1)) {
			anim.SetBool ("attackMode", true);
		} else {
			anim.SetBool ("attackMode", false);
		}

	} 
}
