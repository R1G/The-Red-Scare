using UnityEngine;
using System.Collections;

public class MasterEnemyScript : MonoBehaviour {

	int enemyHealth;
	int enemyDamage;
	GameObject selectedTroop;
	public bool isWithinAttackRange;
	public GameObject gameController;

	float playerX;
	float playerZ;
	float meleeX;
	float meleeZ;

	void Start() {
		enemyHealth = 10;
		enemyDamage = 10;
	}

	void CheckAttackRange() {
		playerX = selectedTroop.transform.position.x;
		playerZ = selectedTroop.transform.position.z;

		meleeX = Mathf.Abs (gameObject.transform.position.x - playerX);
		meleeZ = Mathf.Abs (gameObject.transform.position.z - playerZ);

		if (1 >= meleeX) {
			if (1 >= meleeZ) {
				isWithinAttackRange = true;
			} else {
				isWithinAttackRange = false;
			}
		} else {
			isWithinAttackRange = false;
		}
	}

	void EnemyDeath() {
		if (enemyHealth <= 0) {
			Destroy(gameObject);
		}
	}

	void Update() {
		selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");
		if (GameScript.turn == "EnemyTurn") {
			transform.Translate(0,0,1);
			GameScript.turn = "PlayerTurn";
		}

	}

	void OnMouseUp() {
		if (selectedTroop != null) {
			CheckAttackRange ();
			if (isWithinAttackRange) {
				enemyHealth -= enemyDamage;
				EnemyDeath();
			}
		}
	}
}
