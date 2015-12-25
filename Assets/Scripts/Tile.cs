using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// Constructor
	public Tile(string prefab, Vector3 pos) {
		GameObject tilePrefabGameObject = Instantiate(Resources.Load(prefab), pos, Quaternion.Euler(270, 0, 0)) as GameObject;
	}
}