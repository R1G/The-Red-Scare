using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TileGenerator : MonoBehaviour {

	public static int mapCol;
	public static int mapRow;

	public static GameObject [,] tilesRef;	// Holds a reference to every instantiated tile
	public static int range = 3;
	public static List<GameObject> highlightedTiles = new List<GameObject>(); // Holds a reference to every highlighted tile

	Quaternion tileRot = Quaternion.Euler(270, 0, 0);

	string[] tileTypes = new string[] {
		"FF9900",	// Dirt_Block
		"000000",	// unassigned
		"FF0000",	// Rock_Block
		"000000"	// unassigned
	};

	// Prefab resources
	public string[] prefabResources = new string[4] {
		"Dirt_Block",
		"Dirt_Slab",
		"Rock_Block",
		"Rock_Slab"
	};

	void Start () {
		// Load an image of a map and use its pixels as the array
		Texture2D levelBitmap = Resources.Load("map") as Texture2D;
		mapRow = levelBitmap.height;
		mapCol = levelBitmap.width;

		// Hold a reference to every instantiated tile in a new game object
		tilesRef = new GameObject[mapRow, mapCol];

		// Loop throw the pixels and draw the map based on each pixel's color
		for (int x = 0; x < mapRow; x++) {
			for (int z = 0; z < mapCol; z++) {
				// Get the color of the current pixel
				Color c = levelBitmap.GetPixel(z, x);

				// Convert the color (which will be RGB) to it's hex value
				string hex = ColorUtility.ToHtmlStringRGB(c);

				// Obtain a tile index from tile types to get it's prefab resource
				int tileIndex = System.Array.IndexOf(tileTypes, hex);
				Vector3 tilePos = new Vector3 (x, 0, z);
				GameObject prefab = Resources.Load(prefabResources[tileIndex]) as GameObject;

				// Instantiate a tile and store it in the tiles ref for later use
				tilesRef[x, z] = Instantiate(prefab, tilePos, tileRot) as GameObject;
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