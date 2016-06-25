using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyMelee : MasterEnemyScript {

	float meleeDmg = 5f;
	float meleeHP = 10f;
	bool hasAttacked = false;

	private MasterTroopScript troop;

	public override void Start() {
		base.Start ();
		enemyHealth = meleeHP;
		enemyDamage = meleeDmg;
	}

	/*public override IEnumerator Update() {
		base.Update ();

		if (GameScript.turn == "EnemyTurn") {

			GameObject[] friendlyList = GameObject.FindGameObjectsWithTag("FriendlyTroop");
			foreach ( GameObject entry in friendlyList) {

						if(hasAttacked == false) {
							Debug.Log ("Enemy Melee Unit: attacking!");
							troop = entry.GetComponent<MasterTroopScript>();
							troop.dmg (meleeDmg);
							hasAttacked = true;
				}
			}
		}

		if (GameScript.turn == "PlayerTurn") 
			hasAttacked = false;
	}
*/
}
