using UnityEngine;
using System.Collections;

public class TextRotationScript : MonoBehaviour {

	Camera camera = Camera.main;

	void Start() {
		camera = Camera.main;
	}

	void Update () {
		
		transform.LookAt (camera.gameObject.transform.position);
	}
}
