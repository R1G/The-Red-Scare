using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TileGenerator : MonoBehaviour {

	public static int mapCol;
	public static int mapRow;
	public TextAsset mapInfo;

	public static GameObject [,] tilesRef;	// Holds a reference to every instantiated tile
	public static int range = 3;
	public static List<GameObject> highlightedTiles = new List<GameObject>(); // Holds a reference to every highlighted tile

	//Tile[] tileTypes;
	Quaternion tileRot = Quaternion.Euler(270, 0, 0);
	
	// Prefab resources
	public string[] prefabResources = new string[6] {
		"Dirt_Block",
		"Dirt_Ramp",
		"Dirt_Slab",
		"Grass_Block",
		"Grass_Ramp",
		"Grass_Slab"
	};

	int[,] tileMap = new int[15,15];

	void Start () {

		mapRow = tileMap.GetUpperBound(0) + 1;
		mapCol = tileMap.GetUpperBound(1) + 1;

		tilesRef = new GameObject[mapRow, mapCol];


		for (int x = 0; x < mapRow; x++) {
			for (int z = 0; z < mapCol; z++) {
				int tileIndex = tileMap[x, z];
				// Get already created prefab from Unity and position/rotate it
				Vector3 tilePos = new Vector3 (x, 0, z);
				GameObject prefab = Resources.Load(prefabResources[tileIndex]) as GameObject;

				// Instantiate a tile and store it in the tiles ref for later use
				GameObject tile = Instantiate(prefab, tilePos, tileRot) as GameObject;
				tilesRef[x, z] = tile;
			}
		}
	}

	// Highlight an area of tiles around the selected troop
	public static void highlightTilesInRange(int x, int z) {
		unhighlightTiles();
		// Compute non-zero lower and upper bounds to loop within tiles ref
		int lowerBoundRow = x - range;
		if (lowerBoundRow < 0) {
			lowerBoundRow = 0;
		}

		int upperBoundRow = x + range; 
		if (upperBoundRow > mapRow) {
			upperBoundRow = mapRow;
		}


		int lowerBoundCol = z - range;
		if (lowerBoundCol < 0) {
			lowerBoundCol = 0;
		}

		int upperBoundCol = z + range + 1;

		if (upperBoundCol > mapCol) {
		upperBoundCol = mapCol;
		}


		for (int r = lowerBoundRow; r <= upperBoundRow; r++) {
			if(mapCol -r > 0) {
			for (int c = lowerBoundCol; c < upperBoundCol; c++) {
				if (mapCol - c > 0) {
					tilesRef [r, c].GetComponent<MeshRenderer> ().material.color = Color.green;
					highlightedTiles.Add (tilesRef [r, c]);
					}
				}
			}
		}
	}

	public static void unhighlightTiles() {
		if (highlightedTiles.Count > 0) {
			foreach (GameObject tile in highlightedTiles) {
				tile.GetComponent<MeshRenderer> ().material.color = Color.white;
			}
			highlightedTiles.Clear();
		}
	}


}