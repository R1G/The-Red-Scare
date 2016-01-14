using UnityEngine;
using System.Collections;

public class MasterTroopScript : MonoBehaviour   {
	
	float troopHealth;
	float troopDamage;

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
		troopHealth = 20;
		troopDamage = 10;
	}

	void Update() {
		//If the first logged position does not equal the current position of the unit, then it has moved
		if (originalPos != gameObject.transform.position) {
			MasterEnemyScript.addFriendlyPos (identifier, transform.position);
			originalPos = gameObject.transform.position;
			troopDeath();
			Debug.Log("Player Health: "+ troopHealth);
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

	public void dmg(float nDmg) {
		troopHealth -= nDmg;
	}

	private void troopDeath() {
		if (troopHealth <= 0)
			Destroy (gameObject);
	}

	private void assignIdentifier() {
		identifier = gameObject.name;

	}
}
