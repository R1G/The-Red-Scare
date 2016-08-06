using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapConstructor : MonoBehaviour {

	GameObject[] buildings = new GameObject[7];
	GameObject[] cityBlock;

	void Awake() {
		buildings [0] = Resources.Load ("Cube") as GameObject;
		buildings [1] = Resources.Load ("Cube2") as GameObject;
		buildings [2] = Resources.Load ("Cube3") as GameObject;
		buildings [3] = Resources.Load ("Cube4") as GameObject;
		buildings [4] = Resources.Load ("Cube5") as GameObject;
		buildings [5] = Resources.Load ("Empty") as GameObject;
		buildings [6] = Resources.Load ("Cube6") as GameObject;
		FindCityBlocks ();
		BuildCityBuildings ();
	}

	void FindCityBlocks() {
		Debug.Log ("Getting city blocks");
		cityBlock = GameObject.FindGameObjectsWithTag ("Block");
	}

	void BuildCityBuildings() {
		Debug.Log ("Building new Levittburg");
		for (int i = 0; i < cityBlock.Length; i++) {
			int randomBuildingChoice = Random.Range (0, buildings.Length);
			Instantiate (buildings[randomBuildingChoice], cityBlock [i].transform.position, Quaternion.identity);
		}
	}
}
