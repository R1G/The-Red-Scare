using UnityEngine;
using System.Collections;

public class TileGenerator : MonoBehaviour {

	int mapCol;
	int mapRow;
	TileType[] tileTypes;


	int[,] tileMap = new int[,]
	{
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0}
	};

	// Prefab resources
	string[] prefabResources = new string[6] {
		"Dirt_Block",
		"Dirt_Ramp",
		"Dirt_Slab",
		"Grass_Block",
		"Grass_Ramp",
		"Grass_Slab"
	};

	// Use this for initialization
	void Start () {
		int mapCol = tileMap.GetUpperBound(1);
		int mapRow = tileMap.GetUpperBound(0);

		for (int x = 0; x <= mapRow; x++) {
			for (int z = 0; z <= mapCol; z++) {
				int tileIndex = tileMap[x, z];

				// Get already created prefab from Unity and postition/rotate it
				Vector3 tilePos = new Vector3 (x * 2, 0, z * 2);
				Quaternion tileRot = Quaternion.Euler(270, 0, 0);

				// Instantiate a new tile with this prefab
				GameObject tilePrefabGameObject = Instantiate(Resources.Load(prefabResources[tileIndex]), tilePos, tileRot) as GameObject;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
