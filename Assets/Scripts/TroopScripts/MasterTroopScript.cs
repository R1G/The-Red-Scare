using UnityEngine;
using System.Collections;

public class MasterTroopScript : MonoBehaviour {
	
	int troopHealth;
	int troopDamage;
	float travelRange;
	float attackRange;
	bool isSelectedTroop;


	GameObject enemyTroop;
	public GameObject gameController;
	public GameObject selectedTroop;

	void Start() {
		gameObject.tag = "FriendlyTroop";
	}

	void SetAsSelectedUnit() {
		gameObject.tag = "SelectedTroop";
		TileGenerator.highlightTilesInRange((int)gameObject.transform.position.x, (int)gameObject.transform.position.z);
	}

	public void UnsetAsSelectedUnit () {
		gameObject.tag = "FriendlyTroop";
		TileGenerator.unhighlightTiles();
	}

	void OnMouseUp() {
		if (GameScript.turn != "PlayerTurn") {
			return;
		}

		selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");

		if (selectedTroop == null) {
			SetAsSelectedUnit();
		}

		if (selectedTroop == gameObject) {
			// the current object is selected
			UnsetAsSelectedUnit();
		} else if(selectedTroop != null) {
			selectedTroop.tag = "FriendlyTroop";
			SetAsSelectedUnit();
		}
	}
}
