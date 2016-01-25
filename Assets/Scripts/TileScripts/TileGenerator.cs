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

	Quaternion tileRot = Quaternion.Euler(0, 0, 0);

	string[] tileTypes = new string[] {
		"C26900", // wall – nonwalkable – red team wall
		"ff770b", // brazier – nonwalkable – red team ground glowing artifact
		"BA960A", // bush – nonwalkable – bush outside red team
		"FFC40F", // dirt – walkable – red team outer ground
		"C5BEAC", // water - empty
		"789D4C", // grass – walkable – center and alternate corners
		"FF8A00", // lava- walkable – red team base ground
		"E9BA00", // shrub – walkable – area surrounding bush outside red team
		"58623C", // tree - nonwalkable
		"737373", // shrinestone – walkable – top left & bottom right corner base center
		"595959", // shrine - nonwalkable
		"9D694C", // road – walkable – red team road/path
		"5D442A", // bridge – walkable – red team bridge/crossing
		"295C55", // crossing – walkable – blue team bridge/crossing
		"D7D7D7", // gravel – walkable – center area alternate quadrants
		"909090", // crossroad – walkable – center area crossroads
		"3B949E", // tundra – walkable – blue team outer ground
		"ACC5BE", // path – walkable – blue team road/path
		"005B9B", // rampart – nonwalkable – blue team wall
		"727272", // pillar – nonwalkable – center area alternate quadrant prop
		"FBBB10", // red buff – walkable – center area lower bottom quadrant
		"3C7B7D", // teal house – non-walkable – center area house of the Teal family
		"727B67", // gray house – non-walkable – center area house of the Gray family
		"816A39", // brown house – non wallable – center area house of the Indian family
		"5D5D5D", // fountain – non walkable – center most artifact (animated fountain)
		"1CC0D3", // glow stone – non walkable – blue team ground glowing artifact
		"04E2FD", // blue buff – walkable – center area top right quadrant
		"397980", // fog – walkable – fog around menhirs outside blue team base
		"39666F", // menhir – nonwalkable – menhirs outside blue team base
		"0096FF"  // ice – walkable – blue team base ground
	};

	// Prefab resources
	string[] prefabResources = new string[] {
		"wall", 			// nonwalkable – red team wall
		"brazier", 			// nonwalkable – red team ground glowing artifact
		"bush", 			// nonwalkable – bush outside red team
		"dirt", 			// walkable – red team outer ground
		"water", 			// empty
		"grass", 			// walkable – center and alternate corners
		"lava", 			// walkable – red team base ground
		"shrub", 			// walkable – area surrounding bush outside red team
		"tree", 			// nonwalkable
		"shrineStone", 		// walkable – top left & bottom right corner base center
		"shrine", 			// nonwalkable
		"road", 			// walkable – red team road/path
		"bridge", 			// walkable – red team bridge/crossing
		"crossing", 		// walkable – blue team bridge/crossing
		"gravel", 			// walkable – center area alternate quadrants
		"crossRoad", 		// walkable – center area crossroads
		"tundra", 			// walkable – blue team outer ground
		"path", 			// walkable – blue team road/path
		"rampart", 			// nonwalkable – blue team wall
		"pillar", 			// nonwalkable – center area alternate quadrant prop
		"redBuff", 		// walkable – center area lower bottom quadrant
		"tealHouse", 		// non-walkable – center area house of the Teal family
		"grayHouse", 		// non-walkable – center area house of the Gray family
		"brownHouse", 		// non wallable – center area house of the Indian family
		"fountain", 		// non walkable – center most artifact (animated fountain)
		"glowStone", 		// non walkable – blue team ground glowing artifact
		"blueBuff", 		// walkable – center area top right quadrant
		"fog", 				// walkable – fog around menhirs outside blue team base
		"menhir", 			// nonwalkable – menhirs outside blue team base
		"ice" 				// walkable – blue team base ground
	};

	void Start () {

		// Load an image of a map and use its pixels as the array
		Texture2D levelBitmap = Resources.Load("mapPlan") as Texture2D;
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
						if (tilesRef [r, c].tag == "walkableTile") {
							tilesRef [r, c].GetComponent<MeshRenderer> ().material.color = Color.green;
							highlightedTiles.Add (tilesRef [r, c]);
						}
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