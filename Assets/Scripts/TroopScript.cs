using UnityEngine;
using System.Collections;

public class TroopScript : MonoBehaviour {

	public int health;
	public int damage;

	void Start() {
		gameObject.tag = "FriendlyTroop";
	}

	void OnMouseUp() {
		gameObject.tag = "SelectedTroop";
		Debug.Log ("Selected");
	}

	void Update() {
		if (transform.hasChanged) {
			gameObject.tag = "FriendlyTroop";
			transform.hasChanged = false;
		}
	}
}
