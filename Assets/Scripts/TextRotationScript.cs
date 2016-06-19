using UnityEngine;
using System.Collections;

public class TextRotationScript : MonoBehaviour {

	public Camera camera;

	void Start() {
		camera = Camera.main;
	}

	void Update () {
		transform.LookAt (camera.gameObject.transform.position);
	}
}
