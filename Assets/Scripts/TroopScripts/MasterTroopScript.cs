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

	void Start() {
		gameController = GameObject.FindGameObjectWithTag ("PlayerTurn");
	}

	void SetAsSelectedUnit() {
		if (gameController != null) {
			gameObject.tag = "SelectedTroop";
			isSelectedTroop = true;
		}
	}

	void OnMouseUp() {
		if (isSelectedTroop) {
			gameObject.tag = "FriendlyTroop";
			isSelectedTroop = false;
		} else if (!isSelectedTroop) {
			gameObject.tag = "SelectedTroop";
			isSelectedTroop = true;
		}

	}
}
