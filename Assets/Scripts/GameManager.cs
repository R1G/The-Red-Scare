using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject[] AI;
	GameObject[] communists;
	int communistNumber = 0;

	void Start() {
		FindCommunists ();
		Debug.Log (communistNumber);
	}

	void FindCommunists() {
		for (int i = 0; i < 8; i++) {
			if (AI [i].tag == "Communist") {
				communistNumber++;
			}
		}
	}
}
