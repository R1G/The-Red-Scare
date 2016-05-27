using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	Animator anim;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void Update() {
		float horizontal;
		float vertical;
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		transform.Translate (0, 0, vertical*speed);
		transform.Rotate (0, horizontal, 0);

		anim.SetFloat ("speed", vertical);
	}

}
