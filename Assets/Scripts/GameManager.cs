using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

	public GameObject citizen;
	public GameObject crime;
	public float detectiveSkill;
	GameObject[] spawnPoints;
	public DossierDataClass dossier;
	public string dossierText;
	public bool dossierActive;
	public static int population = 20;

	GameObject[] citizens = new GameObject[population];
	List<GameObject> communists = new List<GameObject> (); 
	GameObject[] buildings;

	float crimeCooldown = 60f;
	float cooldownRemaining = 0f;
	public Text score;
	public Vector2 scrollPosition;

	public static int communistPower = 45;
	int defaultCommunistPower = 20;

	int communistLimit;

	// TODO: put smarter code for determining how many communists there are at game start
	//For example, there can nenver be more than 10 or less than 2
	void Start() {
		dossierActive = false;
		dossier = ScriptableObject.CreateInstance<DossierDataClass> ();
		score.text = communistPower.ToString ();
		spawnPoints = GameObject.FindGameObjectsWithTag ("WayPoint");
		SetBuildings ();

		Debug.Log ("Spawning Citizens");
		for (int i = 0; i < population; i++) {
			int spawnPointChoice = Random.Range(0, 16);
			citizens[i] =(GameObject) Instantiate (citizen, spawnPoints[spawnPointChoice].transform.position, Quaternion.identity);
		}

		FindCommunists ();
		Debug.Log (communists.Count);
	}

	void Update(){
		//The communist limit is determined by the communist power divided by five, as can be seen here
		//This determines the real number of citizens who are also communists
		communistLimit = communistPower / 5;
		BalanceCommunists();

		HandleCrimes ();
		UpdateScore ();
	}

	void FindCommunists() {
		//Takes all the citizens, and finds the magnitude of their communist characteristic
		//If it is at or above five, it adds them to the communism group
		foreach (GameObject person in citizens) {
			AIScript script = person.GetComponent<AIScript> ();

			if (script.GetCommunism () >= 5) communists.Add (person);
		}
	}

	void BalanceCommunists(){
		//This method makes sure that the number of communists never exceeds the communist limit, and tries to add new communists when possible
		//the first if statement finds the current communist with the lowest communism characteristic, and removes it from the communist list
		//The second simply tries to add more communist to the list
		//This method activates every frame, so hopefully it won't affect the fps. If not we can optimise it later
		if (communists.Count > communistLimit) {
			int communismCheck = 100;
			GameObject deleteTarget = communists [0];
			foreach (GameObject person in communists) {
				AIScript script = person.GetComponent<AIScript> ();
				if (script.GetCommunism() < communismCheck) {
					deleteTarget = person;
					communismCheck = script.GetCommunism ();
				}
			}
			 communists.Remove (deleteTarget);
		}

		if (communists.Count < communistLimit) {
			FindCommunists ();
		}
	}

	void SetBuildings(){
		buildings = GameObject.FindGameObjectsWithTag ("Building");
	}

	void HandleCrimes(){
		cooldownRemaining -= Time.deltaTime;
		if (cooldownRemaining <= 0) {
			GameObject newCrime = (GameObject) Instantiate (crime, Vector3.zero, Quaternion.identity);
			CrimeDataClass crimeData = newCrime.GetComponent<CrimeDataClass>();
			dossier.dossierCrimeEntries.Add (crimeData);
			SetBuildings ();
			crimeData.SetData ("arson", communists, communistPower, buildings);
			communistPower += 10;
			cooldownRemaining = AdjustCrimeCooldown(crimeCooldown);
			//dossier.UpdateDossier ();
		}
	}

	void UpdateScore() {
		if (communistPower != defaultCommunistPower) {
			score.text = communistPower.ToString ();
			Debug.Log (communistPower);
			defaultCommunistPower = communistPower;
			if (communistPower <= 0) {
				Debug.Log ("You Win! No more communists in Levittburg!");
				SceneManager.LoadScene ("WinningScene");

			}
			if (communistPower >= 100) {
				Debug.Log ("You lose! The radicals have overthrown Levittburg!");
				SceneManager.LoadScene ("LosingScene");
			}
		}
	}

	float AdjustCrimeCooldown(float currentCooldown) {
		float cooldown = currentCooldown * 50*50/communistPower/communistPower;
		return cooldown;
	}

	void OnGUI() {
		scrollPosition = GUILayout.BeginScrollView (new Vector2(200,250), GUILayout.Width (450), GUILayout.Height (500));
		GUILayout.Label (dossierText);
		GUILayout.EndScrollView ();
		if (dossierActive) {	
			dossierText = dossier.GetDossierText ();
		} else {
			dossierText = "";
		}
	}

	public void ToggleDossier() {
		if (dossierActive != true) {
			dossierActive = true;
		} else {
			dossierActive = false;
		}
	}
}

