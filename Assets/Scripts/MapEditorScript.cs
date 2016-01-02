using UnityEngine;
using System.Collections;

public class MapEditorScript : MonoBehaviour {

	int mapArraySizeX;
	int mapArraySizeY;
	
	public static int[,] tileEditorField = new int[,] {
	};

	void Start() {
		for (int x = 0; x <= 10; x++) {
			for (int y = 0; y <= 10; y++) {
				Vector3 tilePos = new Vector3(x,y,0);
				GameObject emptyTile = Resources.Load("Empty_Tile") as GameObject;
				Instantiate(emptyTile, tilePos, Quaternion.identity);
			}
		}
	}


	/* 	On play, the script will instantiate empty gameObjects with boxColliders. 
	 * On MouseUp, the empty gameObject will be destroyed, and replaced with the selected prefab.
	 * 		The tileEditorField will also be updated to the new tileType, based on the transform.position
	 */

}
