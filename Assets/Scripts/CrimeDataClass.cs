using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrimeDataClass : MonoBehaviour {

	static int crimeNumber = 0;

	bool inProgress;

	string crimeType;
	List<GameObject> perpetrators;
	GameObject perpetrator;
	AIScript perpScript;
	int communistPower;
	GameObject[] buildings;

	// Use this for initialization
	void Start () {
		crimeNumber++;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (gameObject.transform.position);
	
	}

	//TODO: Add code for when a citizen is within range of the crime

	public void setData (string crimeType, List<GameObject> perpetrators, int communistPower, GameObject[] buildings){
		
		this.crimeType = crimeType;
		this.perpetrators = perpetrators;
		this.communistPower = communistPower;
		this.buildings = buildings;

		GameObject building = buildings[Random.Range(0,buildings.Length)];
		Debug.Log (building.transform.position);
		gameObject.transform.position = building.transform.position;
		gameObject.transform.rotation = building.transform.rotation;

		startCrime();
		
	}


	void startCrime() {
		perpetrator = selectPerpetrator ();
		perpScript = perpetrator.GetComponent<AIScript> ();

		perpScript.commitCrime (gameObject.transform.position, gameObject.GetComponent<CrimeDataClass>());

	}

	GameObject selectPerpetrator(){
		return perpetrators [Random.Range (0, perpetrators.Count)];
	}

	public void crimeActive(){
		inProgress = true;
	}

	public void crimeDeactive(){
	}
}
