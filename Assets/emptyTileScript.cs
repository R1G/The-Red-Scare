using UnityEngine;
using System.Collections;

public class emptyTileScript : MonoBehaviour {

	Vector3 tilePosition;

	void OnMouseDown() {
		tilePosition = gameObject.transform.position;
		GameObject selectedTile = Instantiate (Resources.Load ("Grass_Block"), tilePosition, Quaternion.identity) as GameObject;

	}
}
