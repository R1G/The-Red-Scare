using UnityEngine;
using System.Collections;

public class MasterTroopScript : MonoBehaviour   {
	

	float troopHealth;
	//float troopDamage;

	public static int travelRange;

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
		//troopDamage = 10;
	}

	void SetAsSelectedUnit() {
		gameObject.tag = "SelectedTroop";
		TileGenerator.highlightTilesInRange((int)gameObject.transform.position.x, (int)gameObject.transform.position.z);
		if (originalPos != gameObject.transform.position) {
			MasterEnemyScript.addFriendlyPos (identifier, transform.position);
			originalPos = gameObject.transform.position;
			troopDeath();
		}
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
