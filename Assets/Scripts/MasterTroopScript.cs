using UnityEngine;
using System.Collections;

public class MasterTroopScript : MonoBehaviour {
	
	int troopHealth;
	int troopDamage;
	float travelRange;

	void SetAsSelectedUnit() {
		gameObject.tag = "SelectedTroop";
	}

	void OnMouseUp() {
		SetAsSelectedUnit ();
	}


}
