using UnityEngine;

public class TileGen : MonoBehaviour {

	GameObject selectedUnit;
	public TileType[] tileTypes;
	int[,] tiles;

	public int mapSizeX = 10;
	public int mapSizeY = 10;

	void Start() {
		tiles = new int[mapSizeX, mapSizeY];

		for (int x = 0; x < mapSizeX; x++) {
			for(int y = 0; y < mapSizeY; y++){
				tiles[x,y] = 0;
			}
				//insert addition tile instructions here
			tiles[0,6] = 1;
			tiles[1,6] = 1;
			tiles[2,6] = 1;
			tiles[3,6] = 1;
			tiles[4,6] = 1;
			tiles[5,6] = 1;
			tiles[6,6] = 1;
			tiles[7,6] = 1;
			tiles[8,6] = 1;
			tiles[9,6] = 1;

			tiles[0,7] = 5;
			tiles[1,7] = 5;
			tiles[2,7] = 5;
			tiles[3,7] = 5;
			tiles[4,7] = 5;
			tiles[5,7] = 5;
			tiles[6,7] = 5;
			tiles[7,7] = 5;
			tiles[8,7] = 5;
			tiles[9,7] = 5;

			tiles[0,8] = 5;
			tiles[1,8] = 5;
			tiles[2,8] = 5;
			tiles[3,8] = 5;
			tiles[4,8] = 5;
			tiles[5,8] = 5;
			tiles[6,8] = 5;
			tiles[7,8] = 5;
			tiles[8,8] = 5;
			tiles[9,8] = 5;

			tiles[0,9] = 5;
			tiles[1,9] = 5;
			tiles[2,9] = 5;
			tiles[3,9] = 5;
			tiles[4,9] = 5;
			tiles[5,9] = 5;
			tiles[6,9] = 5;
			tiles[7,9] = 5;
			tiles[8,9] = 5;
			tiles[9,9] = 5;

			tiles[0,10] = 5;
			tiles[1,10] = 5;
			tiles[2,10] = 5;
			tiles[3,10] = 5;
			tiles[4,10] = 5;
			tiles[5,10] = 5;
			tiles[6,10] = 5;
			tiles[7,10] = 5;
			tiles[8,10] = 5;
			tiles[9,10] = 5;

			tiles[0,11] = 5;
			tiles[1,11] = 5;
			tiles[2,11] = 5;
			tiles[3,11] = 5;
			tiles[4,11] = 5;
			tiles[5,11] = 5;
			tiles[6,11] = 5;
			tiles[7,11] = 5;
			tiles[8,11] = 5;
			tiles[9,11] = 5;

		}

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
