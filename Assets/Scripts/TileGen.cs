using UnityEngine;

public class TileGen : MonoBehaviour {

	GameObject selectedUnit;
	public TileType[] tileTypes;
	int[,] tiles;
	int DIRT_RAMP = 1;
	int GRASS_SLAB = 5;

	public int mapSizeX = 10;
	public int mapSizeY = 10;

	void Start() {
		tiles = new int[mapSizeX, mapSizeY];

		for (int x = 0; x < mapSizeX; x++) {
			for(int y = 0; y < mapSizeY; y++){
				tiles[x,y] = 0;
			}
		}

		//insert addition tile instructions here
		tiles[0,6] = DIRT_RAMP;
		tiles[1,6] = DIRT_RAMP;
		tiles[2,6] = DIRT_RAMP;
		tiles[3,6] = DIRT_RAMP;
		tiles[4,6] = DIRT_RAMP;
		tiles[5,6] = DIRT_RAMP;
		tiles[6,6] = DIRT_RAMP;
		tiles[7,6] = DIRT_RAMP;
		tiles[8,6] = DIRT_RAMP;
		tiles[9,6] = DIRT_RAMP;
		
		tiles[0,7] = GRASS_SLAB;
		tiles[1,7] = GRASS_SLAB;
		tiles[2,7] = GRASS_SLAB;
		tiles[3,7] = GRASS_SLAB;
		tiles[4,7] = GRASS_SLAB;
		tiles[5,7] = GRASS_SLAB;
		tiles[6,7] = GRASS_SLAB;
		tiles[7,7] = GRASS_SLAB;
		tiles[8,7] = GRASS_SLAB;
		tiles[9,7] = GRASS_SLAB;
		
		tiles[0,8] = GRASS_SLAB;
		tiles[1,8] = GRASS_SLAB;
		tiles[2,8] = GRASS_SLAB;
		tiles[3,8] = GRASS_SLAB;
		tiles[4,8] = GRASS_SLAB;
		tiles[5,8] = GRASS_SLAB;
		tiles[6,8] = GRASS_SLAB;
		tiles[7,8] = GRASS_SLAB;
		tiles[8,8] = GRASS_SLAB;
		tiles[9,8] = GRASS_SLAB;
		
		tiles[0,9] = GRASS_SLAB;
		tiles[1,9] = GRASS_SLAB;
		tiles[2,9] = GRASS_SLAB;
		tiles[3,9] = GRASS_SLAB;
		tiles[4,9] = GRASS_SLAB;
		tiles[5,9] = GRASS_SLAB;
		tiles[6,9] = GRASS_SLAB;
		tiles[7,9] = GRASS_SLAB;
		tiles[8,9] = GRASS_SLAB;
		tiles[9,9] = GRASS_SLAB;
		
		tiles[0,10] = GRASS_SLAB;
		tiles[1,10] = GRASS_SLAB;
		tiles[2,10] = GRASS_SLAB;
		tiles[3,10] = GRASS_SLAB;
		tiles[4,10] = GRASS_SLAB;
		tiles[5,10] = GRASS_SLAB;
		tiles[6,10] = GRASS_SLAB;
		tiles[7,10] = GRASS_SLAB;
		tiles[8,10] = GRASS_SLAB;
		tiles[9,10] = GRASS_SLAB;
		
		tiles[0,11] = GRASS_SLAB;
		tiles[1,11] = GRASS_SLAB;
		tiles[2,11] = GRASS_SLAB;
		tiles[3,11] = GRASS_SLAB;
		tiles[4,11] = GRASS_SLAB;
		tiles[5,11] = GRASS_SLAB;
		tiles[6,11] = GRASS_SLAB;
		tiles[7,11] = GRASS_SLAB;
		tiles[8,11] = GRASS_SLAB;
		tiles[9,11] = GRASS_SLAB;

		createTileVisual ();
	}

	void Update() {
		selectedUnit = GameObject.FindGameObjectWithTag("SelectedTroop");
	}

	void createTileVisual() {
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {

				TileType tt = tileTypes [tiles [x, y]];
				GameObject GO = (GameObject)Instantiate (tt.tileVisualPrefab, new Vector3 (x * 2, 0, y * 2), Quaternion.Euler(270,0,0));
				ClickableTile ct = GO.GetComponent<ClickableTile> ();
				//Used to define where the tile is in the array
				ct.tileX = x;
				ct.tileY = y;
				//instance of this class. Needed by ClickableTile.cs
				ct.map = this;
			}
		}
	}
		//Replace with path-finding
	public void MoveSelectedUnitTo(int x, int y) {
		if (selectedUnit != null) {
			selectedUnit.transform.position = new Vector3 (x*2, 0, y*2);
		}
	}
}
