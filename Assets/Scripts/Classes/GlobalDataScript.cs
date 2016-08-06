using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalDataScript : MonoBehaviour {

	public static string[] firstNames  = {"Jeffrey", "Johnny", "Joan", "Johanna", "Jane", "Jack", "Janice", "Jacques", "Jeremy", "Jill", "Jesse", "Jonah", "Jim", "Jacob", "Jo", "June"};
	public static string[] lastNames = {"Depp", "Djikstra", "Davidson", "Donner", "Dickens", "Doppler", "DeVito", "DuHast"};

	public static Vector3[] wayPoints; 
	public static GameObject[] wayPointObjs = GameObject.FindGameObjectsWithTag("WayPoint");
	public static GameObject[] buildings;


	static void Awake() {
		for (int i = 0; i < wayPointObjs.Length; i++) {
			wayPoints [i] = wayPointObjs [i].transform.position;
		}
	}

	static void Start() {
		buildings = GameObject.FindGameObjectsWithTag ("Building");
	}

	public static void MoveToWayPoint(GameObject citizen) {
		int length = wayPointObjs.Length;
		int wayPointChoice = Random.Range (0, length);
		NavMeshAgent agent = citizen.GetComponent<NavMeshAgent> ();
		agent.SetDestination (wayPointObjs [wayPointChoice].transform.position);
	}

	public static string GenerateName() {
		int firstNameChoice = Random.Range (0, firstNames.Length);
		int lastNameChoice = Random.Range (0, lastNames.Length);
		string name = firstNames[firstNameChoice] + " " + lastNames[lastNameChoice];
		return name;
	}
	public static bool GetRandomBool() {
		int boolNumber = Random.Range (0, 2);
		if (boolNumber == 0) {
			return true;
		} else {
			return false;
		}
	}

	public static TraitDataClass PickRandomTrait(AIScript perpScript) {
		int traitNumber = Random.Range (0, 4);
		TraitDataClass[] traits = { perpScript.coat, perpScript.glasses, perpScript.hat, perpScript.hair };
		TraitDataClass traitChoice = traits [traitNumber];
		return traitChoice;

	}
}
