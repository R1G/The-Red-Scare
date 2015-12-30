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
		gameController = GameObject.FindGameObjectWithTag ("PlayerTurn");
		selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");
		if (gameController != null) {
			gameObject.tag = "SelectedTroop";
			if(selectedTroop != null) {
				selectedTroop.tag = "FriendlyTroop";
			}
		}
	}

	void OnMouseUp() {
		SetAsSelectedUnit ();
	}
}
