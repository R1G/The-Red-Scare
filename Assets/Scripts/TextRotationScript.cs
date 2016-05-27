using UnityEngine;
using System.Collections;

public class TextRotationScript : MonoBehaviour {

	public Camera camera;

	void Update () {
		transform.LookAt (camera.gameObject.transform.position);
	}
}
