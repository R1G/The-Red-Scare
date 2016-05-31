using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIScript : MonoBehaviour {

	NavMeshAgent agent;
	public GameObject gameManager;
	GameObject[] WayPoints;

	//parameter variables set randomly in Start()
	public bool isCommunist;
	public bool isHonest;
	public bool isViolent;

	//state variables
	bool isSelected = false;
	bool isAttacking = false;
	bool isFleeing = false;

	//player unit
	public GameObject detective;

	//empty transforms that AI travels between
	public GameObject WayPoint1;
	public GameObject WayPoint2;
	public GameObject WayPoint3;
	public GameObject WayPoint4;

	//Reference for pathfinding 
	int wayPointChoice;

	void Start() {
		//Assigned publicly through the editor
		WayPoints = new GameObject[4]{WayPoint1, WayPoint2, WayPoint3, WayPoint4};

		agent = GetComponent<NavMeshAgent> ();
		//for the three parameters violent, honest, communist
		int choice1 = Random.Range (0, 2);
		int choice2 = Random.Range (0, 2);
		int choice3 = Random.Range (0, 2);
		//initial destination chosen
		int wayPointChoice = Random.Range (0, 4);

		if (choice1 == 0) {
			isCommunist = true;
			gameObject.tag = "Communist";
		} 

		if (choice2 == 0) {
			isHonest = true;
		}

		if (choice3 == 0) {
			isViolent = true;
		}

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
			AnswerQuestion ();
		}
	}

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

	void Attack() {
		isAttacking = true;
		agent.SetDestination (detective.transform.position);
	}

	void Confess() {
		Debug.Log (gameObject.name + " has confessed his Communist nature");
	}

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

	void Escape() {
		Debug.Log (gameObject.name + " is attempting to fleeeeeeee");
		agent.Resume ();
		agent.speed = 5f;
		isFleeing = true;
	}

}
