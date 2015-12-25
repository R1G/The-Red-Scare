using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int enemyHealth;
	public int enemyDamage;
	GameObject playerTroop;


	public float distanceX;
	public float distanceZ;

	public float meleeRangeX;
	public float meleeRangeZ;

	public bool selectedUnitExists;

	void Start() {
		enemyDamage = 10;
		enemyHealth = 10;
	}

	void Update() {
		playerTroop = GameObject.FindGameObjectWithTag("SelectedTroop");

		DefineDistances ();
		//AttackCheck ();
		EnemyDeath ();
	}

	void OnMouseUp() {
		if (selectedUnitExists) {
			enemyHealth -= enemyDamage;
		}
	}

	void EnemyDeath() {
		if (enemyHealth <= 0) {
			Destroy(gameObject);
		}
	}

	void AttackCheck() {
		if (playerTroop != null) {
			if (distanceX <= meleeRangeX) {
				if (distanceZ <= meleeRangeZ) {
					selectedUnitExists = true;
				}
			}
		} else {
			selectedUnitExists = false;
		}
	}

	void DefineDistances() {
		if (playerTroop != null) {
			float distanceX = Mathf.Abs (playerTroop.transform.position.x - gameObject.transform.position.x);
			float distanceZ = Mathf.Abs (playerTroop.transform.position.z - gameObject.transform.position.z);

			if(distanceX <= 2) {
				if(distanceZ <= 2) {
					selectedUnitExists = true;
				}
			}
		}
	}
}
