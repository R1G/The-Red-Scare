using UnityEngine;
using System;
using System.Collections;

public class BaseTile : MonoBehaviour {

	public GameObject selectedTroop;
	public bool isWithinTravelRange;
	public GameObject gameController;

	void Update() {
		FindSelectedTroop ();
		if (selectedTroop != null) {
			CheckRange ();
			if (isWithinTravelRange) {
				SetSelectedTileColor ();
			} else {
				SetRegularTileColor ();
			}
		} else {
			SetRegularTileColor();
		}
	}
	
	void OnMouseUp() {
		FindSelectedTroop ();
		CheckRange ();
		if (isWithinTravelRange) {
			MoveSelectedTroop ();
		} 
	}

	public void FindSelectedTroop() {
		selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");
	}

	public void CheckRange() {
		if (selectedTroop != null) {
			float troopPositionX = selectedTroop.transform.position.x;
			float troopPositionZ = selectedTroop.transform.position.z;
			
			float tilePositionX = gameObject.transform.position.x;
			float tilePositionZ = gameObject.transform.position.z;
			
			float distanceX = Mathf.Abs (troopPositionX - tilePositionX);
			float distanceZ = Mathf.Abs (troopPositionZ - tilePositionZ);
			
			if (3 >= distanceX) {
				if (3 >= distanceZ) {
					isWithinTravelRange = true;
				}  else {
					isWithinTravelRange = false;
				}
			} else {
				isWithinTravelRange = false;
			}
		}
	}

	public void MoveSelectedTroop() {
		if (GameScript.turn == "PlayerTurn" && selectedTroop != null) {
			selectedTroop.transform.position = gameObject.transform.position;
			selectedTroop.tag = "FriendlyTroop";
			selectedTroop = null;
			GameScript.turn = "EnemyTurn";
		}
	}

	public void SetSelectedTileColor() {
		gameObject.GetComponent<MeshRenderer> ().material.color = Color.green;
	}

	public void SetRegularTileColor() {
		gameObject.GetComponent<MeshRenderer> ().material.color = Color.white;
	}
}
