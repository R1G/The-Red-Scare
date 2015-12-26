using UnityEngine;
using System.Collections;

public class MasterTroopScript : MonoBehaviour {
	
	int troopHealth;
	int troopDamage;
	float travelRange;
	float attackRange;

	GameObject enemyTroop;

	void SetAsSelectedUnit() {
		gameObject.tag = "SelectedTroop";
	}

	void OnMouseUp() {
		SetAsSelectedUnit ();
	}
}
