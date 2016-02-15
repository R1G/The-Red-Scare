using UnityEngine;
using System.Collections;

public class RoundPosition : MonoBehaviour {

	void FixPosition() {
		gameObject.transform.position = new Vector3 ((float)Mathf.RoundToInt(transform.position.x),0,(float)Mathf.RoundToInt(transform.position.z));	
	}

	void Start() {
		InvokeRepeating ("FixPosition",2,20);
	}
}
