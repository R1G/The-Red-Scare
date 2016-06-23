using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour {

	public string buildingType;
	string name;

	// Use this for initialization
	void Start () {
		name = gameObject.name;
		/*string subName = name.Substring(0,(name.Length-4));
		if (subName == "Post_Office") {
			buildingType = "Post_Office";
		} */
	}
	
	// Update is called once per frame
	void Update () {

	}
}
