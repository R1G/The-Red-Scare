using UnityEngine;
using System.Collections;

public class BaseTile : MonoBehaviour {

	GameObject selectedTroop;
	Collider tileClickBox;
	public float travelRange = 6;
	public bool isWithinTravelRange;
	

	void Update() {
		selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");

	}

	void CheckRange() {
		float troopPositionX = selectedTroop.transform.position.x;
		float troopPositionZ = selectedTroop.transform.position.z;

		float tilePositionX = gameObject.transform.position.x;
		float tilePositionZ = gameObject.transform.position.z;

		float distanceX = Mathf.Abs(troopPositionX - tilePositionX);
		float distanceZ = Mathf.Abs(troopPositionZ - tilePositionZ);

		if (travelRange >= distanceX) {
			if (travelRange >= distanceZ) {
				isWithinTravelRange = true;
			} 
		}
	}

	void MoveSelectedTroop() {
		selectedTroop.transform.position = gameObject.transform.position;
	}

	void OnMouseUp() {
		if (selectedTroop != null) {
			CheckRange(); 
			if (isWithinTravelRange) {
				MoveSelectedTroop();
				selectedTroop.tag = "FriendlyTroop";
			}
		}
	}
}
