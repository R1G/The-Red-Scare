using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIScript : MonoBehaviour {

	NavMeshAgent agent;

	GameObject[] WayPoints;

	public bool isCommunist;
	public bool isHonest;
	public bool isViolent;

	bool isSelected;

	public GameObject WayPoint1;
	public GameObject WayPoint2;
	public GameObject WayPoint3;
	public GameObject WayPoint4;

	int wayPointChoice;

	void Start() {

		WayPoints = new GameObject[4]{WayPoint1, WayPoint2, WayPoint3, WayPoint4};

		agent = GetComponent<NavMeshAgent> ();

		int choice1 = Random.Range (0, 2);
		int choice2 = Random.Range (0, 2);
		int choice3 = Random.Range (0, 2);

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
		if (isSelected) {
			agent.Stop ();
		} else {
			agent.Resume ();
		}
		if (agent.remainingDistance <= 2f) {
			int choice = Random.Range (0, 4);
			agent.SetDestination (WayPoints [choice].transform.position);
		}
	}

	void OnMouseDown() {
		if (isSelected) {
			isSelected = false;
			wayPointChoice = Random.Range (0, 4);
			agent.SetDestination (WayPoints [wayPointChoice].transform.position);
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
		Debug.Log (gameObject.name + " is going to attack");
	}

	void Confess() {
		Debug.Log (gameObject.name + " has confessed his Communist nature");
	}

	void AccuseInnocent() {
		Debug.Log (gameObject.name + " has accused " + " of Communism");
	}

	void AccuseGuilty() {
		Debug.Log (gameObject.name + " has accused " + " of Communism");
	}

	void Deny() {
		Debug.Log (gameObject.name + " denies any relation to the Communist party");
	}

	void Escape() {
		Debug.Log (gameObject.name + "is attempting to fleeeeeeee");
	}

}
