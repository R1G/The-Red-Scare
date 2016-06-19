using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIScript : MonoBehaviour {

	NavMeshAgent agent;
	public GameObject gameManager;
	GameObject[] WayPoints;


	int communism = Random.Range (0, 10);
	int honesty = Random.Range (0, 10);
	int violence = Random.Range (0, 10);


	//state variables
	bool isSelected = false;
	bool isAttacking = false;
	bool isFleeing = false;

	//player unit
	public GameObject detective;


	//Is it possible to add these waypoints in the editor to gameobjects that are being instantiated?
	//for now, I changed these to private, and found the waypoints by name
	//probably want to change this later

	GameObject WayPoint1;
	GameObject WayPoint2; 
	GameObject WayPoint3; 
	GameObject WayPoint4;




	//Reference for pathfinding 
	int wayPointChoice;

	void Start() {

		WayPoint1 = GameObject.Find("WayPoint1");
		WayPoint2 =  GameObject.Find("WayPoint2");
		WayPoint3 =  GameObject.Find("WayPoint3");
		WayPoint4 = GameObject.Find("WayPoint4");

		gameObject.tag = "Citizen";

		WayPoints = new GameObject[4]{WayPoint1, WayPoint2, WayPoint3, WayPoint4};

		agent = GetComponent<NavMeshAgent> ();


		// TODO: put smarter code for determining how many communists there are at game start
		//For example, there can nenver be more than 10 or less than 2

		//changed name from choices to actual trait names




		int wayPointChoice = Random.Range (0, 4);



		//Commented out the boolean aspects of the ai traits

		/*
		if (choice1 == 0) {
			isCommunist = true;
			gameObject.tag = "Communist";
		} 

		if (choice2 == 0) {
			isHonest = true;
		}

		if (choice3 == 0) {
			isViolent = true;
		} */

		agent.SetDestination(WayPoints[wayPointChoice].transform.position);
	}



	void Update() {
		if (isAttacking) {
			Attack ();
		} else if (isFleeing) {
			Escape ();
			isFleeing = false;
		} else if (isSelected) {
			
		} else {
			if (agent.remainingDistance <= 1f || agent.destination == null) {
				wayPointChoice = Random.Range (0, 4);
				agent.SetDestination (WayPoints [wayPointChoice].transform.position);
				agent.speed = 10f;
			} else {
				agent.Resume ();
			}
		}
	}

	void OnMouseDown() {
		if (isSelected) {
			isSelected = false;
		} else {
			isSelected = true;
		//	AnswerQuestion ();
		}
	}

	public int getCommunism(){
		return communism;
	}

	//I commented this out as well, just to avoid the errors I'd get from commenting out the earlier bools
	/*
	void AnswerQuestion() {
		if (isCommunist) {
			if (isHonest) {
				if (isViolent) {
					Attack ();
				} else {
					Escape ();
				}
			} else {
				if (isViolent) {
					AccuseInnocent ();
				} else {
					Deny ();
				}

			}
		} else {
			if (isViolent) {
				if (isHonest) {
					AccuseGuilty ();
				} else {
					AccuseInnocent ();
				}
			} else {
				if (isHonest) {
					Deny ();
				} else {
					Escape ();
				}
			}
		}
	}


	*/
	void Attack() {
		isAttacking = true;
		agent.SetDestination (detective.transform.position);
	}

	void Confess() {
		Debug.Log (gameObject.name + " has confessed his Communist nature");
	}

	/*
	void AccuseInnocent() {
		GameObject accused = gameManager.GetComponent<GameManager> ().AI [4];
		Debug.Log (gameObject.name + " has accused " + accused.name + " of Communism");
	}


	void AccuseGuilty() {
		GameObject accused = gameManager.GetComponent<GameManager> ().AI [3];
		Debug.Log (gameObject.name + " has accused " + accused.name + " of Communism");
	}

	void Deny() {
		Debug.Log (gameObject.name + " denies any relation to the Communist party");
		agent.Resume ();
	}
*/

	void Escape() {
		Debug.Log (gameObject.name + " is attempting to fleeeeeeee");
		agent.Resume ();
		agent.speed = 5f;
		isFleeing = true;
	}


}
