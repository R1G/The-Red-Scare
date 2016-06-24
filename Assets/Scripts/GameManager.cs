using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

	public GameObject citizen;
	public GameObject crime;

	public static int population = 10;

	GameObject[] citizens = new GameObject[population];
	List<GameObject> communists = new List<GameObject> (); 
	GameObject[] buildings;

	float crimeCooldown = 1f;
	float cooldownRemaining = 0;

	public int communistPower = 20;

	int communistLimit;

	void Start() {

		setBuildings ();

		for (int i = 0; i < population; i++) {
			//We'll need to instantiate these guys better, as in not all in the same exact place
			//Alternitively, we could instantiate them in the same place, and let the the town run for a minute, 
			//letting the waypoints disperse the NPCs

			citizens[i] =(GameObject) Instantiate (citizen, Vector3.zero, Quaternion.identity);
		}
		

		FindCommunists ();
		Debug.Log (communists.Count);
	}

	void Update(){

		//The communist limit is detirmined by the communist power divided by five, as can be seen here
		//This determines the real number of citiaens who are also communists
		communistLimit = communistPower / 5;
		balanceCommunists();

		handleCrimes ();
	}

	//Takes all the citizens, and finds the magnitude of their communist characteristic
	//If it is at or above five, it adds them to the communism group
	void FindCommunists() {

		foreach (GameObject person in citizens) {
			AIScript script = person.GetComponent<AIScript> ();

			if (script.getCommunism () >= 5) communists.Add (person);
				
		}

		/*
		for (int i = 0; i < 8; i++) {
			if (AI [i].tag == "Communist") {
				communistNumber++;
			}
		} */
	}

	//This method makes sure that the number of communists never exceeds the communist limit, and tries to add new communists when possible
	//the first if statement finds the current communist with the lowest communism characteristic, and removes it from the communist list
	//The second simply tries to add more communist to the list
	//This method activates every frame, so hopefuly it won't affect the fps.If not we can optimise it later

	void balanceCommunists(){
		
		if (communists.Count > communistLimit) {
			int communismCheck = 100;
			GameObject deleteTarget = communists [0];
			foreach (GameObject person in communists) {
				AIScript script = person.GetComponent<AIScript> ();
				if (script.getCommunism() < communismCheck) {
					deleteTarget = person;
					communismCheck = script.getCommunism ();
				}
			}
			 communists.Remove (deleteTarget);
		}

		if (communists.Count < communistLimit) {
			FindCommunists ();
		}
	}

	void setBuildings(){
		buildings = GameObject.FindGameObjectsWithTag ("Building");
	}

	void handleCrimes(){
		cooldownRemaining -= Time.deltaTime;
		if (cooldownRemaining <= 0) {
			GameObject newCrime = (GameObject) Instantiate (crime, Vector3.zero, Quaternion.identity);
			CrimeDataClass crimeData = newCrime.GetComponent<CrimeDataClass>();
			crimeData.setData ("communist", communists, communistPower, buildings);
			cooldownRemaining = crimeCooldown;



		}
	}
}
