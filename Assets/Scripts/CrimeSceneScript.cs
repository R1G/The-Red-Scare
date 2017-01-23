using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrimeSceneScript : MonoBehaviour {

	static int crimeNumber = 0;

	public bool inProgress;

	string crimeType;
	List<GameObject> perpetrators;
	GameObject perpetrator;
	AIScript perpScript;
	int communistPower;
	GameObject[] buildings;
	GameObject building;

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
		
		this.crimeType = crimeType;
		this.perpetrators = perpetrators;
		this.communistPower = communistPower;
		this.buildings = buildings;
		this.name = "Crime Scene";

		//Debug.Log (this.buildings.Length);
		building = buildings[Random.Range(0,buildings.Length)];

		gameObject.transform.position = building.transform.position;
		gameObject.transform.rotation = building.transform.rotation;

		StartCrime();
	}


	void StartCrime() {
		perpetrator = SelectPerpetrator ();
		perpScript = perpetrator.GetComponent<AIScript> ();

		perpScript.CommitCrime (gameObject.transform.position, gameObject.GetComponent<CrimeSceneScript>());
	}

	GameObject SelectPerpetrator(){
		return perpetrators [Random.Range (0, perpetrators.Count)];
	}

	public void CrimeActive(){
		inProgress = true;
	}

	public void CrimeDeactive(){
	}

	int CalculateCrimeLvl() {
		float crimeLvlRand = Random.value;
		float low;
		float mid;
		float high;


		if (communistPower <= 100) {
			low = 0.75f;
			mid = 0.20f;
			high = 0.05f;
		} else if (communistPower <= 200) {
			low = 0.45f;
			mid = 0.30f;
			high = 0.25f;
		} else if (communistPower <= 300) {
			low = 0.20f;
			mid = 0.50f;
			high = 0.30f;
		} else if (communistPower <= 400) {
			low = 0.10f;
			mid = 0.25f;
			high = 0.75f;
		} else {
			low = 0.05f;
			mid = 0.05f;
			high = 0.90f;
		}

		float lvlTrack = 0f;

		if (crimeLvlRand < low)
			return 1;
		if (crimeLvlRand >= low && crimeLvlRand < high)
			return 2;
		if (crimeLvlRand >= high)
			return 3;
		
		return -1;


	}

	public int GetCrimeNumber() {
		return crimeNumber;
	}
}
