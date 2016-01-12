using UnityEngine;
using System.Collections;

public class MasterTroopScript : MonoBehaviour   {
	
	int troopHealth;
	int troopDamage;
	float travelRange;
	float attackRange;
	bool isSelectedTroop;
	Vector3 originalPos;

	GameObject enemyTroop;
	public GameObject gameController;
	public GameObject selectedTroop;


	private string identifier;

	void Start() {
		gameObject.tag = "FriendlyTroop";
		assignIdentifier ();
		MasterEnemyScript.addFriendlyPos (identifier, transform.position);
		originalPos = gameObject.transform.position;
	}

	void Update() {
		//If the first logged position does not equal the current position of the unit, then it has moved
		if (originalPos != gameObject.transform.position) {
			MasterEnemyScript.addFriendlyPos (identifier, transform.position);
			originalPos = gameObject.transform.position;
			GameScript.turn = "EnemyTurn";

		}
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
			MasterEnemyScript.addFriendlyPos (identifier, transform.position);
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

	private void assignIdentifier() {
		identifier = gameObject.name;

	}
}
