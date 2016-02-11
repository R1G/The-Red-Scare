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

		int xPos = 0;
		int zPos = 0;

		// If enemy is to the west of the target pos,
		// then it needs to move 1 unit to the right
		// If enemy is to the east of the target pos,
		// then it needs to move 1 unit to the left
		if (enemyX - targetPosX < 0) {
			xPos = 1;
		} else if (enemyX - targetPosX > 0) {
			xPos = -1;
		}

		// If enemy is toward the north of the target pos,
		// then it needs to move 1 unit down
		// If enemy is toward the south of the target pos,
		// then it needs to move 1 unit up
		if (enemyZ - targetPosZ < 0) {
			zPos = 1;
		} else  if (enemyZ - targetPosZ > 0) {
			zPos = -1;
		}
			
		if (TileGenerator.tilesRef [enemyX + xPos, enemyZ + zPos].tag == "walkableTile") {
			return new Vector3 (xPos, 0, zPos);
		} else {
			// Computed tile is not walkable
			return new Vector3 (0, 0, 0);
		}
	}

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
