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
		selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");
		if (GameScript.turn == "PlayerTurn") {
			gameObject.tag = "SelectedTroop";
			if(selectedTroop != null) {
				GameScript.turn = "FriendlyTroop";
			}
		}
	}

	void OnMouseUp() {
		SetAsSelectedUnit ();
	}
}
