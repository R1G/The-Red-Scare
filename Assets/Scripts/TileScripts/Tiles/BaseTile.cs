using UnityEngine;
using System;
using System.Collections;

public class BaseTile : MonoBehaviour {

	private GameObject selectedTroop;
	private bool isWithinTravelRange;

	void OnMouseUp() {
		FindSelectedTroop();
		if(selectedTroop != null) {
			CheckRange();
			if(isWithinTravelRange) {
				MoveSelectedTroop();
				TileGenerator.unhighlightTiles ();
			}
		}
	}

	void FindSelectedTroop() {
		selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");
	}

	void CheckRange() {
		int troopX = (int)selectedTroop.transform.position.x;
		int troopZ = (int)selectedTroop.transform.position.z;

		int distanceX = troopX - (int)gameObject.transform.position.x;
		int distanceZ = troopZ - (int)gameObject.transform.position.z;

		if (8 >= distanceX) {
			if (8 >= distanceZ) {
				isWithinTravelRange = true;
			}
		}
	}

	void MoveSelectedTroop() {
		float tileX = gameObject.transform.position.x;
		float tileZ = gameObject.transform.position.z;

		selectedTroop.transform.position = new Vector3 (tileX, 0, tileZ);
	}

}
