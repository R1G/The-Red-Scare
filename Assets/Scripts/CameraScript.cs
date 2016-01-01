using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float mouseSensitivity;

	void Update() {
		float horizontal = Input.GetAxis ("Horizontal") * mouseSensitivity;
		float vertical = Input.GetAxis ("Vertical") * mouseSensitivity;
		transform.Translate (horizontal, vertical, 0);	
	}
}
