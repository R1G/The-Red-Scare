using UnityEngine;
using System.Collections;

public class emptyTileScript : MonoBehaviour {

	Vector3 tilePosition;

	void OnMouseDown() {
		tilePosition = gameObject.transform.position;
		Instantiate (Resources.Load ("Grass_Block"), tilePosition, Quaternion.identity);

		MapEditorScript.editorTileField [(int)tilePosition.x, (int)tilePosition.y] = 1;
	}
}
