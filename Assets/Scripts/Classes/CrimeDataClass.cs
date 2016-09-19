using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrimeDataClass : MonoBehaviour {

	static int crimeNumber = 0;
	int witnessNumber = 0;
	public DossierDataClass dossier;
	public List<ClueDataClass> crimeClues = new List<ClueDataClass> ();
	public string crimeName;
	//bool inProgress;

	//string crimeType;
	List<GameObject> perpetrators;
	GameObject perpetrator;
	AIScript perpScript;
	//int communistPower;
	//GameObject[] buildings;

	// Use this for initialization
	void Start () {
		crimeNumber++;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (gameObject.transform.position);
	
	}

	//TODO: Add code for when a citizen is within range of the crime

	public void SetData (string crimeType, List<GameObject> perpetrators, int communistPower, GameObject[] buildings){
		
		//this.crimeType = crimeType;
		this.perpetrators = perpetrators;
		//this.communistPower = communistPower;
		//this.buildings = buildings;

		GameObject building = buildings[Random.Range(0,buildings.Length)];
		gameObject.transform.position = building.transform.position;
		gameObject.transform.rotation = building.transform.rotation;
		this.crimeName = "The " + building.GetComponent<BuildingScript>().buildingType + " " + crimeType;
		StartCrime();
		
	}

	void StartCrime() {
		perpetrator = SelectPerpetrator ();
		perpScript = perpetrator.GetComponent<AIScript> ();

		perpScript.CommitCrime (gameObject.transform.position, gameObject.GetComponent<CrimeSceneScript>());

	}

	GameObject SelectPerpetrator(){
		GameObject perp = perpetrators [Random.Range (0, perpetrators.Count)];
		GameManager.currentPerpetrator = perp;
		return perp;
	}

	public void CrimeActive(){
		//inProgress = true;
	}

	void OnMouseDown() {
		CrimeDeactive ();
		CreateClue ();
	}

	public void CrimeDeactive(){
		Debug.Log ("Investigating scene");
	}

	public ClueDataClass CreateClue() {
		ClueDataClass crimeSceneClue = ScriptableObject.CreateInstance ("ClueDataClass") as ClueDataClass;
		crimeSceneClue.Init (gameObject, GlobalDataScript.PickRandomTrait(perpetrator.GetComponent<AIScript>()));
		crimeClues.Add (crimeSceneClue);
		//dossier.UpdateDossier ();
		perpetrator.GetComponent<ParticleSystem> ().maxParticles = 1000;
		GameManager.currentPerpetrator.GetComponent<AIScript> ().IncreaseEmission (2);
		Debug.Log (GameManager.currentPerpetrator.name);
		return crimeSceneClue;
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("Object has entered a crime scene");
		if (other.gameObject.tag == "Citizen" && witnessNumber <= 30) {
			Debug.Log ("A witness has been added");
			witnessNumber++;
			other.gameObject.GetComponent<AIScript> ().IncreaseEmission (2);
			other.gameObject.GetComponent<AIScript> ().isWitness = true;
		}
	}
}
