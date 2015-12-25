using UnityEngine;
using System.Collections.Generic;

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
				tiles[x,y] = Random.Range(0, 5);
			}
				//insert addition tile instructions here
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
