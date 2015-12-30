using UnityEngine;
using System.Collections;

public class MasterTroopScript : MonoBehaviour {
	
	int troopHealth;
	int troopDamage;
	float travelRange;
	float attackRange;

	GameObject enemyTroop;
	public GameObject gameController;

	void Start() {
		gameController = GameObject.FindGameObjectWithTag ("PlayerTurn");
	}

	void SetAsSelectedUnit() {
		if (gameController != null) {
			gameObject.tag = "SelectedTroop";
		}
	}

	void OnMouseUp() {
		SetAsSelectedUnit ();
	}
}
