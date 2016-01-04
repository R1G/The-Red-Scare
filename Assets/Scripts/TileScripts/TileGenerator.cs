 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TileGenerator : MonoBehaviour {

	int mapCol;
	int mapRow;

	int[,] spaces;

	public static GameObject [,] tilesRef;	// Holds a reference to every instantiated tile
	public static int range = 3;
	public static List<GameObject> highlightedTiles = new List<GameObject>(); // Holds a reference to every highlighted tile

	//Tile[] tileTypes;
	Quaternion tileRot = Quaternion.Euler(270, 0, 0);
	
	// Prefab resources
	public string[] prefabResources = new string[7] {
		"Empty_Tile",
		"Dirt_Block",
		"Dirt_Ramp",
		"Dirt_Slab",
		"Grass_Block",
		"Grass_Ramp",
		"Grass_Slab"
	};

	int[,] tileMap = new int[25,25];

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

		int upperBoundRow = x + range + 1;

		int lowerBoundCol = z - range;
		if (lowerBoundCol < 0) {
			lowerBoundCol = 0;
		}

		int upperBoundCol = z + range + 1;

		for(int r = lowerBoundRow; r < upperBoundRow; r++) {
			for(int c = lowerBoundCol; c < upperBoundCol; c++) {
				tilesRef[r, c].GetComponent<MeshRenderer> ().material.color = Color.green;
				highlightedTiles.Add(tilesRef[r, c]);
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

	private int[,] ParseFile(string filePath) {
		
		string input = System.IO.File.ReadAllText (filePath);
		string[] lines = input.Split (new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		int[,] spaces = new int[lines.Length, 10];
		
		for (int i = 0; i < lines.Length; i++) {
			string st = lines[i];
			string[] nums = st.Split(new[] { ',' });
			for (int j = 0; j < Mathf.Min (nums.Length, 10); j++) {
				int val;
				if (int.TryParse (nums[j], out val))
					spaces[i,j] = val;
				else
					spaces[i,j] = 0;
				
			}
		}
		return spaces;
	}
}