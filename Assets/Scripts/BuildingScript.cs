using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingScript : MonoBehaviour {

	public string buildingType;
	string buildName;
	List<GameObject> owners = new List<GameObject> ();


	//we need to fill some crimes into here
	List<CrimeDataClass> lvlOneCrimes;
	List<CrimeDataClass> lvlTwoCrimes;



	// Use this for initialization
	void Start () {
		buildName = gameObject.name;
		/*string subName = name.Substring(0,(name.Length-4));
		if (subName == "Post_Office") {
			buildingType = "Post_Office";
		} */


	}
	
	// Update is called once per frame
	void Update () {

	}


	public void addCitizen(GameObject citizen){
		owners.Add (citizen);

	}

	//TODO: Assign an owner to the buildings. They will start inside of the building

}
