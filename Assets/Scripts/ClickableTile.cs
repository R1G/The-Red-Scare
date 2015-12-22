using UnityEngine;
using System.Collections;


public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileGen map;
	GameObject selectedUnit;
	//public GameObject player;

	public int travelRange;

	void OnMouseUp() {
		Debug.Log ("Click");
		//Replace with path-finding
		map.MoveSelectedUnitTo (tileX,tileY);
	}

	void Start() {
		gameObject.GetComponent<BoxCollider> ().enabled = false;
		travelRange = 6;
		gameObject.GetComponent<Light> ().enabled = false;
	}

	void Update() {

		selectedUnit = GameObject.FindGameObjectWithTag("SelectedTroop");

		if (selectedUnit != null) {
			float distanceX = selectedUnit.transform.position.x - gameObject.transform.position.x;
			float distanceY = selectedUnit.transform.position.z - gameObject.transform.position.z;
			
			distanceX = Mathf.Abs (distanceX);
			distanceY = Mathf.Abs (distanceY);
			
			if (travelRange >= distanceX && travelRange >= distanceY) {
				gameObject.GetComponent<BoxCollider> ().enabled = true;
				gameObject.GetComponent<Light> ().enabled = true;
			} else {
				gameObject.GetComponent<BoxCollider> ().enabled = false;
				gameObject.GetComponent<Light> ().enabled = false;
			}
		}
		}

}
