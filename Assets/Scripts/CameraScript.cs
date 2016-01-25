using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float mouseSensitivity;

	void Update() {
		// Input.GetAxis returns a float value between -1 and 1
		float horizontal = Input.GetAxis ("Horizontal") * mouseSensitivity * -1;
		float vertical = Input.GetAxis ("Vertical") * mouseSensitivity;

		transform.Translate (vertical, 0, horizontal);

		// Grab updated position of the camera
		float xPos = transform.position.x;
		float zPos = transform.position.z;

		// Reset camera position in case it gets out of bounds
		if (xPos < 0) xPos = 0;
		if (zPos < 0) zPos = 0;

		if (xPos > TileGenerator.mapRow) xPos = TileGenerator.mapRow;
		if (zPos > TileGenerator.mapCol) zPos = TileGenerator.mapCol;

		// Update the position if need be
		if (xPos != transform.position.x || zPos != transform.position.z) {
			transform.position = new Vector3(xPos, 15, zPos);
		}
	}
}
