using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class treeScript : MonoBehaviour {

	void Start () {
		transform.Rotate (0f, (float)Random.Range(0,360),0f);
	}
}
