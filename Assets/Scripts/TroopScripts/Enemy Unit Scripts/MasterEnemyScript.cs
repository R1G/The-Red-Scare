using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Note: For pathfinding in the future, it might be a good idea to have a pathfinding interface that all units implement
//The pathfinding could get pretty complicated eventually, so it might be good to keep it seperate

public class MasterEnemyScript : MonoBehaviour {

	GameObject selectedTroop;
	public bool isWithinAttackRange;
	public GameObject gameController;
	static int nEnemies;
	static int nMoved;

	public float enemyHealth;
	public float enemyDamage;

	float playerX;
	float playerZ;
	float meleeX;
	float meleeZ;
	float targetPosX;
	float targetPosZ;

	private static Dictionary<string, Vector3> troopPosMap = new Dictionary<string, Vector3>();

	public virtual void Start() {
		nEnemies = 5;
	}

	private void CheckAttackRange() {
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

	private void EnemyDeath() {
	
		if(enemyHealth <= 0) Destroy(gameObject);
	}

	public virtual void Update() {
		//selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");
		if (GameScript.turn == "EnemyTurn") {
			findShortestDistance();
			Vector3 movement = determineDirection();
			//The enemy unit will not move if adjacent to it's target's position
			if(gameObject.transform.position.x > (targetPosX + 1) || gameObject.transform.position.x < (targetPosX - 1) 
			   || gameObject.transform.position.z > (targetPosZ + 1) || gameObject.transform.position.z < (targetPosZ - 1)) {
				for (int t = 0; t < 1; t++) {
					transform.Translate(movement);

				}
			}
			nMoved++;
			if(allEnemiesHaveMoved() == true) {
				GameScript.turn = "PlayerTurn";
				nMoved = 0;
			}
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
	//This method maps the identifier of a friendly troop (it's name) to it's position
	//This allows an enemy to determine the closest troop to attack
	//Later we can add more information to the identifier, to, for example, determine the different between archers and melee
	public static void addFriendlyPos(string identifier, Vector3 pos) {
		if (troopPosMap.ContainsKey (identifier) == true) {
			troopPosMap.Remove (identifier);
			troopPosMap.Add (identifier, pos);
		} else {
			troopPosMap.Add (identifier, pos);
		}
	}


	private void findShortestDistance() {
		
		Vector3 smallestPos = findSmallest();

		targetPosX = smallestPos.x;
		targetPosZ = smallestPos.z;
	}

	private Vector3 determineDirection() {
		int enemyX = (int)gameObject.transform.position.x;
		int enemyZ = (int)gameObject.transform.position.z;
		Vector3 defaultMovement = new Vector3 (0,0,0);

		if (enemyX < targetPosX && enemyZ < targetPosZ) {
			if (TileGenerator.tilesRef [enemyX + 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 0);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (0, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, -1);
				return movement;
			} else {
				return defaultMovement;
			}
		} else if (enemyX < targetPosX && enemyZ == targetPosZ) {
			if (TileGenerator.tilesRef [enemyX + 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 0);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (0, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX, enemyZ - 1]) {
				Vector3 movement = new Vector3 (0, 0, -1);
				return movement;
			} else {
				return defaultMovement;
			}

		} else if (enemyX == targetPosX && enemyZ < targetPosZ) {
			if (TileGenerator.tilesRef [enemyX, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (0, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 0);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 0);
				return movement;
			} else {
				return defaultMovement;
			}

		} else if (enemyX > targetPosX && enemyZ > targetPosZ) {
			if (TileGenerator.tilesRef [enemyX - 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 0);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (0, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 1);
				return movement;
			} else {
				return defaultMovement;
			}

		} else if (enemyX > targetPosX && enemyZ == targetPosZ) {
			if (TileGenerator.tilesRef [enemyX - 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 0);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (0, 0, -1);
				return movement;
			} else {
				return defaultMovement;
			}

		} else if (enemyX == targetPosX && enemyZ > targetPosZ) {
			if (TileGenerator.tilesRef [enemyX, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (0, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 0);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ]) {
				Vector3 movement = new Vector3 (1, 0, 0);
				return movement;
			} else {
				return defaultMovement;
			}

		} else if (enemyX > targetPosX && enemyZ < targetPosZ) {
			if (TileGenerator.tilesRef [enemyX - 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, 0);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (0, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 1);
				return movement;
			} else {
				return defaultMovement;
			}
		} else if (enemyX < targetPosX && enemyZ > targetPosZ) {
			if (TileGenerator.tilesRef [enemyX + 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 0);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (0, 0, -1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX + 1, enemyZ + 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (1, 0, 1);
				return movement;
			} else if (TileGenerator.tilesRef [enemyX - 1, enemyZ - 1].tag == "walkableTile") {
				Vector3 movement = new Vector3 (-1, 0, -1);
				return movement;
			} else {
				return defaultMovement;
			}
		} else {
			return defaultMovement;
		}



	}

	//This determines which way the enemy should move
	//The unit has to move different depending on whether it's target is behind them, in front of them, diagonal to them, etc.
	/*private Vector3 determineDirection() {		
		int enemyX = (int)gameObject.transform.position.x;
		int enemyZ = (int)gameObject.transform.position.z;

		if (gameObject.transform.position.x < targetPosX && gameObject.transform.position.z < targetPosZ && TileGenerator.tilesRef [enemyX + 1, enemyZ + 1].tag == "walkableTile") {
			Vector3 movement = new Vector3 (1, 0, 1);
			Debug.Log (TileGenerator.tilesRef [enemyX + 1, enemyZ + 1].tag);
			Debug.Log (TileGenerator.tilesRef [enemyX + 1, enemyZ + 1]);
			return movement;

		} else if (gameObject.transform.position.x > targetPosX && gameObject.transform.position.z > targetPosZ && TileGenerator.tilesRef [enemyX - 1, enemyZ - 1].tag == "walkableTile") {
			Vector3 movement = new Vector3 (-1, 0, -1);
			Debug.Log (TileGenerator.tilesRef [enemyX - 1, enemyZ - 1].tag);
			Debug.Log (TileGenerator.tilesRef [enemyX - 1, enemyZ - 1]);
			return movement;

		} else if (gameObject.transform.position.x > targetPosX && gameObject.transform.position.z < targetPosZ && TileGenerator.tilesRef [enemyX - 1, enemyZ + 1].tag == "walkableTile") {
			Vector3 movement = new Vector3 (-1, 0, 1);
			Debug.Log (TileGenerator.tilesRef [enemyX - 1, enemyZ + 1].tag);
			Debug.Log (TileGenerator.tilesRef [enemyX - 1, enemyZ + 1]);
			return movement;

		} else if (gameObject.transform.position.x < targetPosX && gameObject.transform.position.z > targetPosZ && TileGenerator.tilesRef [enemyX + 1, enemyZ - 1].tag == "walkableTile") {
			Vector3 movement = new Vector3 (1, 0, -1);
			Debug.Log (TileGenerator.tilesRef [enemyX + 1, enemyZ - 1].tag);
			Debug.Log (TileGenerator.tilesRef [enemyX + 1, enemyZ - 1]);
			return movement;

		} else if (gameObject.transform.position.x == targetPosX && gameObject.transform.position.z > targetPosZ && TileGenerator.tilesRef [enemyX, enemyZ - 1].tag == "walkableTile") {
			Vector3 movement = new Vector3 (0, 0, -1);
			Debug.Log (TileGenerator.tilesRef [enemyX, enemyZ - 1].tag);
			Debug.Log (TileGenerator.tilesRef [enemyX, enemyZ - 1]);
			return movement;

		} else if (gameObject.transform.position.x == targetPosX && gameObject.transform.position.z < targetPosZ && TileGenerator.tilesRef [enemyX, enemyZ + 1].tag == "walkableTile") {
			Vector3 movement = new Vector3 (0, 0, 1);
			Debug.Log (TileGenerator.tilesRef [enemyX, enemyZ + 1].tag);
			Debug.Log (TileGenerator.tilesRef [enemyX, enemyZ - 1]);
			return movement;

		} else if (gameObject.transform.position.x < targetPosX && gameObject.transform.position.z == targetPosZ && TileGenerator.tilesRef [enemyX + 1, enemyZ].tag == "walkableTile") {
			Vector3 movement = new Vector3 (1, 0, 0);
			Debug.Log (TileGenerator.tilesRef [enemyX + 1, enemyZ].tag);
			Debug.Log (TileGenerator.tilesRef [enemyX, enemyZ - 1]);
			return movement;

		} else if (gameObject.transform.position.x > targetPosX && gameObject.transform.position.z == targetPosZ && TileGenerator.tilesRef [enemyX - 1, enemyZ].tag == "walkableTile") {
			Vector3 movement = new Vector3 (-1, 0, 0);
			Debug.Log (TileGenerator.tilesRef [enemyX - 1, enemyZ].tag);
			Debug.Log (TileGenerator.tilesRef [enemyX, enemyZ - 1]);
			return movement;

		} else if (gameObject.transform.position.x == targetPosX && gameObject.transform.position.z == targetPosZ && TileGenerator.tilesRef[enemyX, enemyZ].tag == "walkableTile") {
			Vector3 movement = new Vector3 (0, 0, 0);
			Debug.Log (TileGenerator.tilesRef[enemyX, enemyZ].tag);
				Debug.Log (TileGenerator.tilesRef[enemyX, enemyZ]);
			return movement;

		}
		Vector3 defaultMovement = new Vector3 (0,0,0);
		return defaultMovement;
		//Debug.Log ("check");
	} */

	private Vector3 findSmallest() {
		Vector3 vect = new Vector3 (0, 0, 0);
		float distance = 0;

		foreach (KeyValuePair<string, Vector3> entry in troopPosMap) {
			if( Vector3.Distance(entry.Value, gameObject.transform.position) > distance) {
				vect = entry.Value;
				distance = Vector3.Distance(entry.Value, gameObject.transform.position);
			}
		} 
		foreach (KeyValuePair<string, Vector3> entry in troopPosMap) {
			if( Vector3.Distance(entry.Value, gameObject.transform.position) < distance){
				vect = entry.Value;
				distance = Vector3.Distance(entry.Value, gameObject.transform.position);
			}
		} 

		return vect;
	}

	private bool allEnemiesHaveMoved() {
		if (nEnemies == nMoved) {
			return true;
		} else {
			return false;
		}
	}

}
